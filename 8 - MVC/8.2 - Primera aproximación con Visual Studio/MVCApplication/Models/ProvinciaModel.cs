using System.ComponentModel.DataAnnotations;

namespace MVCApplication.Models {
    public class ProvinciaModel {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Descripcion {  get; set; }
    }
}