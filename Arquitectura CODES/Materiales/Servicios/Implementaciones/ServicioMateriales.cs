using ARQ.Datos.Interfaces;
using ARQ.Entidades;
using ARQ.Servicios.Interfaces;
using log4net;
using Servicios.Implementaciones;
using System.Collections.Generic;
using System.Reflection;

namespace ARQ.Servicios.Implementaciones {
    public class ServicioMateriales : ServicioBase<IDatosMateriales>, IServicioMateriales {

        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ServicioMateriales (IDatosMateriales datos) : base(datos) { }

        public IList<Material> ObtenerMateriales (string nombre, string descripcion, double? multiplicador, bool? activo) {
            return this._datos.ObtenerMateriales(nombre, descripcion, multiplicador, activo);
        }

    }

}
