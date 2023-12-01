using Datos.Implementaciones;
using Entidades;
using System.Collections.Generic;

/* LS: Clase para manejar los DatosCategoria luego en el controlador. (PUNTO 4)
 * Unicamente tiene el metodo ObtenerCategorias() */

namespace Servicios.Implementaciones {
    public class ServicioCategoria {

        DatosCategoria _datos;

        public ServicioCategoria () {
            _datos = new DatosCategoria();
        }

        public List<Categoria> ObtenerCategorias() {
            return _datos.ObtenerCategorias();
        }

    }
}
