using ARQ.Entidades;
using System.Collections.Generic;

namespace ARQ.Datos.Interfaces {
    public interface IDatosMateriales : IDatosBase {
        public IList<Material> ObtenerMateriales (string nombre, string descripcion, double? multiplicador, bool? activo);
    }
}
