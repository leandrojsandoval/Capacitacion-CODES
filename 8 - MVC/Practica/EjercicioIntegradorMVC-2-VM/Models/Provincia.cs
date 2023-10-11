using System.ComponentModel.DataAnnotations;

namespace EjercicioIntegradorMVC_2_VM.Models {
    public class Provincia {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Descripcion { get; set; }

        public Provincia(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }

    }
}