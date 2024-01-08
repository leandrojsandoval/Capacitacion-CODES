using ARQ.Framework.Web;

namespace ARQ.Servicios.Interfaces
{
    public interface IServicioLogEventos
    {
        JsonApiData LogInfo(string usuario, string nombreApp, string evento);
    }
}
