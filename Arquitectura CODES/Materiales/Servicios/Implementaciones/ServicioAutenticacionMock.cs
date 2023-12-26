using Framework.Common;
using Newtonsoft.Json;
using ARQ.Framework.Web;
using ARQ.Servicios.Interfaces;

namespace ARQ.Servicios
{
    public class ServicioAutenticacionMock : IServicioAutenticacion
    {
        public JsonApiData AutenticarUsuarioAplicacion(string usuario, string password, string idApp)
        {            
            if (usuario == "CODES\\gfuentes"){

                var data = JsonConvert.SerializeObject(
                                                    new
                                                    {
                                                        id = 917,
                                                        rol = Constantes.ARQ_MANAGER,
                                                        nombre = "Gerardo Fuentes",
                                                        email = "gfuentes@codes.com.ar",
                                                        login = "CODES\\gfuentes"
                                                    });
                return new JsonApiData()
                {
                    result = JsonApiData.Result.Ok,
                    message = data,
                    content = JsonConvert.DeserializeObject<dynamic>(data)
                };
            }
            else {
                return new JsonApiData() { result = JsonApiData.Result.Error, message ="MOCK ERROR!" };
            }
        }

        public JsonApiData AutenticarUsuarioAplicacionAutologin(string usuario, string idApp)
        {
            return this.AutenticarUsuarioAplicacion(usuario, string.Empty, idApp);
        }
    }
}
