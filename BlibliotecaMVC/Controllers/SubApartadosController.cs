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


        [HttpGet]

        public async Task<IActionResult> Editar(int id)
        {
            
            //obtenemos los elementos de la consulta con el Modelo y su herencia
            var obtenerIdSubapartado = await repositorioSubApartados.ObtenerConceptoId(id);

            if (obtenerIdSubapartado is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }


            //Mapeamos los resultados de la consulta anterior con el modelo que trae la coleccion de listado
            // de los apartadod con el modelo anterior de la consulta

            var modelo = new ViewModelListadoApartado()
            {
                ApartadoId = obtenerIdSubapartado.ApartadoId,
                ConceptoID = obtenerIdSubapartado.ConceptoID,
                Concepto = obtenerIdSubapartado.Concepto,
                Normal = obtenerIdSubapartado.Normal,
                ConCredito = obtenerIdSubapartado.ConCredito,
                Unifamiliar = obtenerIdSubapartado.Unifamiliar,
                Redensificacion = obtenerIdSubapartado.Redensificacion
            };

            // una vez mapeados los elementos entro las colecciones, traemos la consulta de los apartados
            // y en este método generamos la selección solo del apartado seleccionado
            modelo.ApartadosLista = await ObtenerListaApartados();

            return View(modelo);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(ViewModelListadoApartado modelo)
        {

            if (!ModelState.IsValid)
            {

                return View(modelo);
            }

            await repositorioSubApartados.Guardar(modelo);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Borrar(int id)
        {
            var Subapartado = await repositorioSubApartados.ObtenerConceptoId(id);

            if (Subapartado is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            //llamamos a la Vista y pasamos el modelo extraido
            return View(Subapartado);
        }


        [HttpPost]
        // invoca desde la vista form asp-action="BorrarSubApartado"
        public async Task<IActionResult> BorrarSubApartado(int ConceptoID)
        {
            var Subapartado = await repositorioApartados.ObtenerPorID(ConceptoID);

            if (Subapartado is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioSubApartados.Borrar(ConceptoID);
            return RedirectToAction("Index");
        }



    }
}

