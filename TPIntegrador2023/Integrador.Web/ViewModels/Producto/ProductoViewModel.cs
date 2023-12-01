using Integrador.Web.ViewModels.Categoria;
using Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/* LS: Agrego el atributo precio para realizar la insercion y modificacion por este campo.
 * Ademas, agrego un nuevos nombree en el paquete de idiomas. (PUNTO 1)
 * Gracias al operador ? que los enteros puedan tener el valor null. 
 *
 * MODIFICADO: Cambio el IDCategoria y Categoria con un objeto CategoriaViewModel que posee
 * ambos atributos. 
 *
 * AGREGADO: Atributo para cargar todas las categorias y mostrarlas en los formularios del a vista
 * Con esto evito usar ViewBag */

namespace Integrador.Web.ViewModels.Producto {

    public class ProductoViewModel : GridViewModel {

        public int Id { get; set; }

        // Ver las verificaciones en el controller

        [Display(Name = "Descripcion", ResourceType = typeof(Global))]
        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "MensajeRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "MensajeStringLength")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio", ResourceType = typeof(Global))]
        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "MensajeRequired")]
        [Range(0, int.MaxValue, ErrorMessage = "MensajeRange")]
        public int? Precio { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        public List<CategoriaViewModel> Categorias { get; set; }

    }

}
