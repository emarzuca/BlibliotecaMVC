using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlibliotecaMVC.Models
{

    //Heredamos el modelo  "Apartado" los datos para general los IEnumerables
    //para mostrarle su listado de cuentas y categorías
    public class ApartadosVierModel: Apartado
    {
        public IEnumerable<SelectListItem> Expedientes { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }
    }
}