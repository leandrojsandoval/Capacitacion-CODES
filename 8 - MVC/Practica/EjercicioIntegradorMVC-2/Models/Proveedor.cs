using Recursos;
using System.ComponentModel.DataAnnotations;

namespace EjercicioIntegradorMVC_2.Models {
    public class Proveedor {

        //[Required]
        public int Id { get; set; }

        [Display(Name = "Proveedor_Nombre_Texto_Mostrar", ResourceType =typeof(GlobalResources))]
        [Required(ErrorMessage = "El nombre del proveedor es requerido.")]
        [StringLength(30, ErrorMessage = "El nombre del proveedor debe ser menor que {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Proveedor_Domicilio_Texto_Mostrar", ResourceType = typeof(GlobalResources))]
        [Required(ErrorMessage = "El domicilio no puede quedar vacio.")]
        [StringLength(50, ErrorMessage = "El domicilio debe ser menor que {1} caracteres.")]
        public string Domicilio { get; set; }

        [Display(Name = "Proveedor_Provincia_Texto_Mostrar", ResourceType = typeof(GlobalResources))]
        [Required(ErrorMessage = "Debe indicar una provincia")]
        public int IdProvincia { get; set; }

        [Display(Name = "Proveedor_Localidad_Texto_Mostrar", ResourceType = typeof(GlobalResources))]
        [Required(ErrorMessage = "Debe indicar una localidad")]
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