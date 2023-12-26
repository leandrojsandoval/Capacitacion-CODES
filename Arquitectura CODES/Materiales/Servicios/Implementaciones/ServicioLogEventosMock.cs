using Framework.Common;
using Newtonsoft.Json;
using ARQ.Framework.Web;
using ARQ.Servicios.Interfaces;

namespace ARQ.Servicios
{
    public class ServicioLogEventosMock : IServicioLogEventos
    {
        public JsonApiData LogInfo(string usuario, string nombreApp, string nombreEvento)
        {
            return new JsonApiData() 
            { 
                content = "MOCK EVENTO LOGUEADO", 
                message = "MOCK EVENTO LOGUEADO",  
                result = JsonApiData.Result.Ok 
            };
        }

    }
}
