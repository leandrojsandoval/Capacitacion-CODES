using System.ComponentModel.DataAnnotations;

namespace EjercicioIntegradorMVC_2_VM.Models {
    public class Localidad {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [Required]
        public int IdProvincia { get; set; }

        public Localidad (int id, string descripcion, int idProvincia) {
            Id = id;
            Descripcion = descripcion;
            IdProvincia = idProvincia;
        }

    }
}