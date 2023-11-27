using EjercicioIntegradorMVC_1.Resources;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace EjercicioIntegradorMVC_1.Models {
    public class Persona {

        // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.stringlengthattribute?view=net-7.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute?view=net-7.0


        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "El campo Id debe ser un número positivo")]
        public int Id { get; set; }

        [Display(Name = "Nombre", ResourceType = typeof(ResourceGlobal))]
        [Required(ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorStringLength")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido", ResourceType = typeof(ResourceGlobal))]
        [Required(ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorStringLength")]
        public string Apellido { get; set; }

        [Display(Name = "TipoDeDocumento", ResourceType = typeof(ResourceGlobal))]
        [Required(ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorList")]
        [Range(1, 3, ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorRange")]
        public int TipoDoc { get; set; }

        [Display(Name = "NumeroDeDocumento", ResourceType = typeof(ResourceGlobal))]
        [Required(ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorRequired")]
        [StringLength(8, ErrorMessageResourceType = typeof(ResourceGlobal), ErrorMessageResourceName = "MensajeErrorStringLengthNumeroDocumento", MinimumLength = 8)]
        public string NroDoc { get; set; }

        public Persona() {}

    }
}