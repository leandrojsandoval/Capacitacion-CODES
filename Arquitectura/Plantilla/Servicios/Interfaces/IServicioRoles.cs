using ARQ.Entidades;
using System.Collections.Generic;

namespace ARQ.Servicios.Interfaces
{
    public interface IServicioRoles
    {
        List<Rol> ObtenerRoles(string idApp);

    }
}
