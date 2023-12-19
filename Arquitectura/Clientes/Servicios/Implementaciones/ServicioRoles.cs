using Framework.Common;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ARQ.Entidades;
using ARQ.Framework.Web;
using ARQ.Servicios.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace ARQ.Servicios
{
    public class ServicioRoles : IServicioRoles
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        IConfiguration Configuration;
        public ServicioRoles(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public List<Rol> ObtenerRoles(string idApp)
        {            
            var httpClient = new HttpClient(new HttpRetryHandler(new HttpClientHandler()), false);
            var urlToSend = this.Configuration[Constantes.SGAASERVICE_KEY_URLRoles];

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, urlToSend);
            requestMessage.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>(){ 
                new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_IDAPP],idApp)
            });

            List<Rol> roles = new List<Rol>();

            var response = httpClient.SendAsync(requestMessage).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var apiResponse = JsonConvert.DeserializeObject<JsonApiData>(responseString);
            roles= JsonConvert.DeserializeObject<List<Rol>>(apiResponse.message);

            return roles;
        }

    }
}
