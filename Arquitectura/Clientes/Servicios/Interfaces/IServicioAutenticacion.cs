using ARQ.Framework.Web;

namespace ARQ.Servicios.Interfaces
{
    public interface IServicioAutenticacion
    {
        JsonApiData AutenticarUsuarioAplicacion(string usuario, string password, string idApp);

        JsonApiData AutenticarUsuarioAplicacionAutologin(string usuario, string idApp);
    }
}
