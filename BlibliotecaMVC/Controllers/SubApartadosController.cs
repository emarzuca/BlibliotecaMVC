using Microsoft.AspNetCore.Mvc;
using BlibliotecaMVC.Servicios;
using BlibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Data.Common;
using Dapper;
using System.Collections;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.AspNetCore;

namespace BlibliotecaMVC.Controllers
{
    public class SubApartadosController : Controller
    {

        public int filtro { get; set; }
        private readonly string connectionString;

        //public SubApartadosController(IConfiguration configuration)
        //{
        //    connectionString = configuration.GetConnectionString("DefaultConnection");
        //}

        private readonly IRepositorioSubApartados repositorioSubApartados;
        private readonly IRepositorioApartados repositorioApartados;
        private readonly IConfiguration configuration;

        public SubApartadosController(IRepositorioSubApartados repositorioSubApartados,
            IRepositorioApartados repositorioApartados, IConfiguration configuration)
        {
            this.repositorioSubApartados = repositorioSubApartados;
            this.repositorioApartados = repositorioApartados;
            this.configuration = configuration;

            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet]
        public async Task<IActionResult> Index(int filtro)
        {

            //IEnumerable<ViewModelCatApartados> listaApartados;

            var subApartados = await ObtenerListaSubApartados(filtro);
            var listaApartados = await repositorioApartados.ListadeApartados2();
            ViewBag.Opciones = listaApartados.ToList();
            return View(subApartados);
        }

        [HttpGet]
        public async Task<IEnumerable<ViewModelSubAparadosEstilos>> ObtenerListaSubApartados(int filtro)
        {
            using var connection = new SqlConnection(connectionString);
            //await VistaApartados(filtro);

            var subApartados = await connection.QueryAsync<ViewModelSubAparadosEstilos>("SP_VisualizaListaSubaparados"
            , new { filtro }, commandType: CommandType.StoredProcedure);

            return subApartados;
        }

        [HttpGet]
        public async Task<IActionResult> NuevaListaFiltroApartado(int filtro)
        {
            //IEnumerable<ViewModelCatApartados> modelo;
            using var connection = new SqlConnection(connectionString);
            //await VistaApartados(filtro);

            var DetalleModeloConceptos = await connection.QueryAsync<ViewModelSubAparadosEstilos>("SP_VisualizaListaSubaparados"
            , new { filtro }, commandType: CommandType.StoredProcedure);
            var modelo = await repositorioApartados.ListadeApartados2();
            ViewBag.Opciones = modelo.ToList();
            return PartialView("_NuevaListaFiltroApartado", DetalleModeloConceptos);

            //return View("Index", nuevoModelo);           
         }



        public async Task<IEnumerable<SelectListItem>> DespliegaAreas()
        {
            var listaAreas = await repositorioApartados.FiltrarArea();
            return listaAreas.Select(x => new SelectListItem(x.Area, x.AreaId.ToString()));
        }

        public async Task<IActionResult> Crear()
        {    
            var modelo = new ViewModelListadoApartado();
            //Obtener Catálogo de Apartados
            modelo.ApartadosLista = await ObtenerListaApartados();
            return View(modelo);
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerListaApartados()
        {
            var ListaApartados = await repositorioApartados.ListadeApartados2();
            return ListaApartados.Select(x => new SelectListItem(x.Apartado, x.ApartadoID.ToString()));
        }


        [HttpPost]

        public async Task<IActionResult> Crear(ViewModelListadoApartado modelo)
        {

            if (!ModelState.IsValid)
            {

                return View(modelo);
            }

            await repositorioSubApartados.Crear(modelo);
            return RedirectToAction("Index");
        }

        //private async Task<IEnumerable<SelectListItem>> ObtenerConContoID
        



    }
}

