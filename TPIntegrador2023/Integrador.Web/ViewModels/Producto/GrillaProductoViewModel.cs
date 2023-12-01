using System.Collections.Generic;

/* LS: Se agregaron los atributos IDCategoria y Categoria, estos son necesarios
 * para cargarlo a la grilla y mostrarlo. IDCategoria no se muestra, pero es 
 * necesario para una modicacion en el caso que el usuario lo requiera. (PUNTO 4) */

namespace Integrador.Web.ViewModels.Producto {
    public class GrillaProductoViewModel {
        public List<ItemViewModel> Items { get; set; }

        public class ItemViewModel {
            public string ID { get; set; }
            public string Descripcion { get; set; }
            public int Precio { get; set; }
            public string IDCategoria { get; set; }
            public string Categoria { get; set; }
            public string FechaAlta { get; set; }
            public bool Activo { get; set; }
        }

    }
}