using System;
using System.ComponentModel.DataAnnotations;

namespace PracticaMVC.Models {

    public class ProvinciaModel {
        [Required]
        [IsValidIdProvincia(ErrorMessage = "El Id de la provincia es inválido")]
        public int Id { get; set; }

        [Required]
        [Display(Name ="NombreProvincia", ResourceType =typeof(GlobalResources))]
        [StringLength(20)]
        public string Descripcion { get; set; }

    }

    public class IsValidIdProvinciaAttribute : ValidationAttribute {
        public override bool IsValid(object value) {
            int id = Convert.ToInt32(value);
            return id > 0;
        }
    }
}