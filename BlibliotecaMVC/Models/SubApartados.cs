using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Schema;
using BlibliotecaMVC.Models;

namespace BlibliotecaMVC.Models
{
    public class SubApartados
    {
        public int ConceptoID { get; set; }
        public string Concepto { get; set; }  
        public int ApartadoId { get; set; }
        public string Apartado { get; set; }
        public bool Unifamiliar { get; set; }
        public  bool Normal { get; set; }
        public bool Redensificacion { get; set; }
        public bool ConCredito { get; set; }      

    }
}
