using System.ComponentModel.DataAnnotations;

namespace BlibliotecaMVC.Models
{
    public class ViewModelDash
    {
        public int ProyectoId { get; set; }
        public int ApartadoId { get; set; }
        public string Apartado { get; set; }
        public int Total { get; set; }
        public int Unifamiliar { get; set; }
        public int Normal { get; set; }
        public int Redensificacion { get; set; }
        public int Concredito { get; set; }

    }
}
