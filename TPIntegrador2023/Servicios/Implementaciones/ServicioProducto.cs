using Datos.Implementaciones;
using Entidades;
using Entidades.Filtros;
using System.Collections.Generic;

namespace Servicios.Implementaciones {
    public class ServicioProducto {

        DatosProducto _datos;

        public ServicioProducto () {
            _datos = new DatosProducto();
        }

        public void Insertar (Producto p) {
            _datos.Insertar(p);
        }

        public void Actualizar (Producto p) {
            _datos.Actualizar(p);
        }

        public void Borrar (int id) {
            _datos.Borrar(id);
        }

        public List<Producto> ObtenerPorFiltro (ProductoFiltro pFiltro) {
            return _datos.ObtenerPorFiltro(pFiltro);
        }

        /* LS: Precio y Categoria son parametros agregados de los que venian por defecto en el proyecto. */
        public List<Producto> ObtenerPaginado (string descripcion, int? precio, int? idCategoria, string sidx, string sord, int page, int rows, out int total) {
            return _datos.ObtenerPaginado(descripcion, precio, idCategoria, sidx, sord, page, rows, out total);
        }
    }
}
