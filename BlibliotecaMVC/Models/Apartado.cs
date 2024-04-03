
using System.ComponentModel.DataAnnotations;

namespace BlibliotecaMVC.Models
{
    public class Apartado
    {
        public int ApartadoId { get; set; }

        [Display(Name = "Nombre Apartado")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} debe ser menor a {1}")]
        [Required(ErrorMessage = "El campo {0} No debe ir vacio")]
        public string apartado { get; set; }


        [Display(Name = "Ruta donde será creado el almacenamiento digital")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} debe ser menor a {1}")]
        public string identificacionDirectorio { get; set; }

        public int AreaId { get; set; }

        [Display(Name = "Área que lo solicitó")]
        public string Area { get; set; }


    }
}