using ARQ.Datos.Interfaces;
using ARQ.Entidades;
using ARQ.Servicios.Interfaces;
using log4net;
using Servicios.Implementaciones;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ARQ.Servicios.Implementaciones {
    public class ServicioClientes : ServicioBase<IDatosClientes>, IServicioClientes {

        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ServicioClientes (IDatosClientes datos) : base(datos) { }

        public IList<Cliente> ObtenerClientes (string nombre, string descripcion, DateTime? fechaNacimiento, bool? activo) {
            return this._datos.ObtenerClientes(nombre, descripcion, fechaNacimiento, activo);
        }

    }
}
