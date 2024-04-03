using Microsoft.AspNetCore.Mvc;
using BlibliotecaMVC.Servicios;
using BlibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace BlibliotecaMVC.Controllers
{
    public class ApartadoController: Controller
    {

        private readonly string connectionString;

        private readonly IRepositorioApartados repositorioApartados;
        private readonly IRepositorioSubApartados repositorioSubApartados;
        private readonly IActionResultTypeMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ICompositeViewEngine viewEngine;
       

        //importar los servicios

        public ApartadoController(IRepositorioApartados repositorioApartados, IRepositorioSubApartados repositorioSubApartados,
            IActionResultTypeMapper mapper, IConfiguration configuration, ICompositeViewEngine viewEngine)
        {
            this.repositorioApartados = repositorioApartados;
            this.repositorioSubApartados = repositorioSubApartados;
            this.mapper = mapper;
            this.configuration = configuration;
            this.viewEngine = viewEngine;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task <IActionResult> Index()
        {
            var apartados = await repositorioApartados.ListadeApartados();
            //var modelo= await DetalleConceptos(13);

            if (apartados is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(apartados);
        }


        //Pantalla de crear
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new ApartadosVierModel();
            //Mapeo de la Información delñ campo areas de ViewModel
            modelo.Areas = await DespliegaAreas();
            return View(modelo);
        }


        [HttpPost]

        public async Task<IActionResult> Crear(ApartadosVierModel Modeloapartado)
        {
            if (!ModelState.IsValid)
            {
                return View(Modeloapartado);
            }

            await repositorioApartados.Crear(Modeloapartado);
            return RedirectToAction("Index");
        }

        public async Task<IEnumerable<SelectListItem>> DespliegaAreas()
        {
            var listaAreas = await repositorioApartados.FiltrarArea();
            return listaAreas.Select(x => new SelectListItem(x.Area, x.AreaId.ToString()));
        }


        public async Task<IActionResult> Borrar(int id)
        {
            var apartado = await repositorioApartados.ObtenerPorID(id);

            if (apartado is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            //llamamos a la Vista y pasamos el modelo extraido
            return View(apartado);
        }


        [HttpPost]

        public async Task<IActionResult> BorrarApartado(int ApartadoId)
        {
            var apartado = await repositorioApartados.ObtenerPorID(ApartadoId);

            if (apartado is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioApartados.Borrar(ApartadoId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var apartado = await repositorioApartados.ObtenerApartadoId(id);


            if (apartado is null)
            {
                return RedirectToAction("NoEncontrado","Home");
            }

            //Mapeo de la Información delñ campo areas de ViewModel a apartado

            var modelo = new ApartadosVierModel()
            {
                ApartadoId = apartado.ApartadoId,
                apartado = apartado.apartado,
                identificacionDirectorio = apartado.identificacionDirectorio,
                AreaId = apartado.AreaId
            };

            modelo.Areas = await ObtenerAreas();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(ApartadosVierModel modeloApartado)
        {
            if (!ModelState.IsValid)
            {
                return View(modeloApartado);
            }
            var apartado = await repositorioApartados.ObtenerApartadoId(modeloApartado.ApartadoId);

            if (apartado is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }            
            await repositorioApartados.Actualizar(modeloApartado);
            return RedirectToAction("index");
        }

        // generamos la funcion para traer el listado de áreas
        private async Task<IEnumerable<SelectListItem>> ObtenerAreas()
        {
            //traer toda la lista
            var listaArea = await repositorioApartados.FiltrarArea();
            return listaArea.Select(x => new SelectListItem(x.Area, x.AreaId.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> DetalleConceptos(int Id) {

            using var connection = new SqlConnection(connectionString);

            if (!ViewExists("_VisualizaConceptoParcial"))
            {
                return NotFound(); // Otra respuesta adecuada, como BadRequest
            }

            try
            {
                IEnumerable<VMDetalleSubApartado> DetalleSubapartadosParcial = await repositorioSubApartados.ObtenerDetalleSubApartados(Id);
                return PartialView("_VisualizaConceptoParcial", DetalleSubapartadosParcial);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si ocurre algún error al obtener los datos
                // Por ejemplo, puedes registrar el error, devolver un mensaje de error al usuario, etc.
                return StatusCode(500, "Error al obtener los datos: " + ex.Message);
            }
        }

                //inyectando ICompositeViewEngine en el constructor del controlador para poder usarlo
        //dentro del método ViewExists.Este enfoque es preferible ya que permite la inyección
        //de dependencias y facilita la prueba unitaria del controlador.

        private bool ViewExists(string viewName)
        {
            var viewResult = viewEngine.FindView(ControllerContext, viewName, false);
            return viewResult.View != null;
        }
    }
}
