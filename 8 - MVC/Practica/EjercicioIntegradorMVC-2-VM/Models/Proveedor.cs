using System.ComponentModel.DataAnnotations;

namespace EjercicioIntegradorMVC_2_VM.Models {
    public class Proveedor {

        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Domicilio { get; set; }
        
        [Required]
        public int IdProvincia { get; set; }
        
        [Required]
        public int IdLocalidad { get; set; }

        public Proveedor(int id, string nombre, string domicilio, int idProvincia, int idLocalidad)
        {
            Id = id;
            Nombre = nombre;
            Domicilio = domicilio;
            IdProvincia = idProvincia;
            IdLocalidad = idLocalidad;
        }
    }
}