using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlibliotecaMVC.Models
{
    public class VMDetalleSubApartado: SubApartados
    {
        public IEnumerable<SelectListItem> DetalleSubApartado { get; set; }

    }
}
