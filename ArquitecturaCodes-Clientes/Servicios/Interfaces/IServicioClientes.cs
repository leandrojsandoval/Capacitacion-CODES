using ARQ.Entidades;
using System;
using System.Collections.Generic;

namespace ARQ.Servicios.Interfaces {
    public interface IServicioClientes {
        public IList<Cliente> ObtenerClientes (string nombre, string apellido, DateTime? fechaNacimiento, bool? activo);
    }
}
