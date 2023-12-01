using Resources;
using System.ComponentModel.DataAnnotations;

/* AGREGADO: Agrego la clase CategoriaViewModel que es atributo de la clase
 * ProductoViewModel. */

namespace Integrador.Web.ViewModels.Categoria {
    public class CategoriaViewModel {

        [Display(Name = "IDCategoria", ResourceType = typeof(Global))]
        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "MensajeRequired")]
        public int? Id { get; set; }

        [Display(Name = "Categoria", ResourceType = typeof(Global))]
        [StringLength(100, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "MensajeStringLength")]
        public string Descripcion { get; set; }

    }
}