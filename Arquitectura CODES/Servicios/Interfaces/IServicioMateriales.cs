using ARQ.Entidades;
using System.Collections.Generic;

namespace ARQ.Servicios.Interfaces {
    public interface IServicioMateriales {
        public IList<Material> ObtenerMateriales (string nombre, string descripcion, double? multiplicador, bool? activo);
    }
}
