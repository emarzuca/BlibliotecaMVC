using BlibliotecaMVC.Servicios;
using Microsoft.AspNetCore.Mvc;
using BlibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace BlibliotecaMVC.Controllers
{
    public class DashController: Controller
    {


        private readonly string connectionString;


        private readonly IRepositorioDash repositorioDash;
        private readonly IConfiguration configuration;

        public DashController( IRepositorioDash repositorioDash, IConfiguration configuration)
        {
            this.repositorioDash = repositorioDash;
            this.configuration = configuration;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDatosGraficos()
        {
            //Obtenemos los datos del Query y del Modelo
            var datos = await repositorioDash.ObtenerDatosParaGrafica();
            // Convierte los datos en un formato que Google Charts pueda entender
            var datosFormatoGoogleCharts = new List<object>();
            datosFormatoGoogleCharts.Add(new[] { "Apartado", "Total","Unifamiliar","Normal","Redensificacion","ConCredito"  }); // Cabecera de la tabla

            foreach (var dato in datos)
            {
                //Insertamos el detalle de la Tabla al objeto list datosFormatoGoogleCharts y añadimos el detalle
                datosFormatoGoogleCharts.Add(new object[] { dato.Apartado, dato.Total, dato.Unifamiliar, dato.Normal, dato.Redensificacion, dato.Concredito});
            }

            return Json(datosFormatoGoogleCharts);
        }

    }
}
