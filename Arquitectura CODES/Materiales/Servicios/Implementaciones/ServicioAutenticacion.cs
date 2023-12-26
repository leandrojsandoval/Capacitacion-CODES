using Framework.Common;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ARQ.Framework.Web;
using ARQ.Servicios.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace ARQ.Servicios {

    public class ServicioAutenticacion : IServicioAutenticacion {

        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        IConfiguration Configuration;
        public ServicioAutenticacion (IConfiguration configuration) {

            this.Configuration = configuration;

        }

        public JsonApiData AutenticarUsuarioAplicacion (string usuario, string password, string idApp) {

            var httpClient = new HttpClient(new HttpRetryHandler(new HttpClientHandler()), false);
            var urlToSend = this.Configuration[Constantes.SGAASERVICE_KEY_URL];

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, urlToSend) {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_USERNAME],usuario),
                    new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_PASSWORD],password),
                    new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_IDAPP],idApp)
                })
            };

            var response = httpClient.SendAsync(requestMessage).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var apiResponse = JsonConvert.DeserializeObject<JsonApiData>(responseString);

            apiResponse.content = apiResponse.result == JsonApiData.Result.Ok ?
                                        JsonConvert.DeserializeObject<dynamic>(apiResponse.message) :
                                        apiResponse.message;

            return apiResponse;

        }

        public JsonApiData AutenticarUsuarioAplicacionAutologin (string usuario, string idApp) {

            var httpClient = new HttpClient(new HttpRetryHandler(new HttpClientHandler()), false);
            var urlToSend = this.Configuration[Constantes.SGAASERVICE_KEY_URL_AUTOLOGIN];

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, urlToSend) {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_USERNAME],usuario),
                    new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_IDAPP],idApp)
                })
            };

            var response = httpClient.SendAsync(requestMessage).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var apiResponse = JsonConvert.DeserializeObject<JsonApiData>(responseString);

            apiResponse.content = apiResponse.result == JsonApiData.Result.Ok ?
                JsonConvert.DeserializeObject<dynamic>(apiResponse.message) :
                apiResponse.message;

            return apiResponse;
        }

    }
}
