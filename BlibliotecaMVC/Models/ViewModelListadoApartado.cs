using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlibliotecaMVC.Models
{
    public class ViewModelListadoApartado:SubApartados
    {
        public IEnumerable<SelectListItem> ApartadosLista { get; set; }
    }
}
