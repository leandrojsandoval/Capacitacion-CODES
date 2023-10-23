using EjercicioIntegradorMVC_1.Resources;
using System.ComponentModel.DataAnnotations;

namespace EjercicioIntegradorMVC_1.Models {
    public class Persona {

        // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.stringlengthattribute?view=net-7.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute?view=net-7.0


        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "El campo Id debe ser un número positivo")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Display(Name = "TipoDeDocumento", ResourceType = typeof(ResourceGlobal))]
        [Required(ErrorMessage = "Se debe indicar un {0} de la lista.")]
        [Range(1, 3, ErrorMessage = "El campo TipoDoc debe ser DNI (1), LE (2) o LC (3).")]
        public int TipoDoc {  get; set; }

        [Display(Name = "NumeroDeDocumento", ResourceType = typeof(ResourceGlobal))]
        [Required]
        [StringLength(8, ErrorMessage = "El campo {0} debe tener {1} numeros.", MinimumLength = 8)]
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