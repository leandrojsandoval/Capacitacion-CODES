using Recursos;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace EjercicioIntegradorMVC_2.Models {
    public class Proveedor {

        //[Required]
        public int Id { get; set; }

        [Display(Name = "Proveedor_Nombre_Texto_Mostrar", ResourceType =typeof(GlobalResources))]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "Mensaje_Error_Required")]
        [StringLength(30, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "Mensaje_Error_String_Length")]
        public string Nombre { get; set; }

        [Display(Name = "Proveedor_Domicilio_Texto_Mostrar", ResourceType = typeof(GlobalResources))]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "Mensaje_Error_Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(GlobalResources) ,ErrorMessageResourceName = "Mensaje_Error_String_Length")]
        public string Domicilio { get; set; }

        [Display(Name = "Proveedor_Provincia_Texto_Mostrar", ResourceType = typeof(GlobalResources))]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "Mensaje_Error_Select_Required")]
        public int IdProvincia { get; set; }

        [Display(Name = "Proveedor_Localidad_Texto_Mostrar", ResourceType = typeof(GlobalResources))]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "Mensaje_Error_Select_Required")]
        public int IdLocalidad { get; set; }

        public Proveedor() {
        }

        public Proveedor(int id, string nombre, string domicilio, int idProvincia, int idLocalidad) {
            Id = id;
            Nombre = nombre;
            Domicilio = domicilio;
            IdProvincia = idProvincia;
            IdLocalidad = idLocalidad;
        }

    }
}