using ARQ.Entidades;
using System;
using System.Collections.Generic;

namespace ARQ.Datos.Interfaces {
    public interface IDatosClientes : IDatosBase {
        public IList<Cliente> ObtenerClientes (string nombre, string apellido, DateTime? fechaNacimiento, bool? activo);
    }
}
