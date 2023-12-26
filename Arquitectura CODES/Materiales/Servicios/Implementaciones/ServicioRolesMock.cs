using log4net;
using Microsoft.Extensions.Configuration;
using ARQ.Entidades;
using ARQ.Servicios.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace ARQ.Servicios
{
    public class ServicioRolesMock : IServicioRoles
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        IConfiguration Configuration;
        public ServicioRolesMock(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public List<Rol> ObtenerRoles(string idApp)
        {
            List<Rol> roles = new() {
                new Rol() { Id = 1, Descripcion = "Manager", Funcion = "General", Activo = true },
                new Rol() { Id = 2, Descripcion = "Turnos", Funcion = "General", Activo = true },
                new Rol() { Id = 3, Descripcion = "Proceso", Funcion = "General", Activo = true },
                new Rol() { Id = 4, Descripcion = "Taller", Funcion = "General", Activo = true }
            };

            return roles;
        }

    }
}
