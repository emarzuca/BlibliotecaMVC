using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlibliotecaMVC.Models
{
    public class ViewModelFiltroSubApartado
    {
        public IEnumerable<SelectListItem> Apartados { get; set; }
    }
}
