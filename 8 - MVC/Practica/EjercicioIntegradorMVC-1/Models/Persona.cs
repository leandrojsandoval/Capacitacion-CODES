using System.ComponentModel.DataAnnotations;

namespace EjercicioIntegradorMVC_1.Models {
    public class Persona {

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "El campo Id debe ser un número positivo")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "El campo TipoDoc debe ser 1, 2 o 3")]
        public int TipoDoc {  get; set; }
        
        [Required]
        public string NroDoc { get; set; }

        public Persona() {
        }

        public Persona(int id, string nombre, string apellido, int tipoDoc, string nroDoc) {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            TipoDoc = tipoDoc;
            NroDoc = nroDoc;
        }
    }
}