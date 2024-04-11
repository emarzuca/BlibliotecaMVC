using System.ComponentModel.DataAnnotations;

namespace BlibliotecaMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [EmailAddress(ErrorMessage ="El campo debe ser un correo electrópnico valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Recordar")]
        public bool Recuerdame { get; set; }
    }
}
