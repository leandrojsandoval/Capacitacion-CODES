using System;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace PracticaAJAX_3 {
    public class handler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            string JSONReceived = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic datosObj = serializer.DeserializeObject(JSONReceived);

            int edad = Convert.ToInt32(datosObj["edad"]);
            string dni = datosObj["dni"];
            string email = datosObj["email"];

            if (edad < 18) {
                context.Response.StatusCode = 400;
                context.Response.Write("{\"result\": -1, \"message\": \"ERROR: El campo [Edad] no es válido, debe tener al menos 18 años\"}");
                return;
            }

            if (!EsDNIValido(dni)) {
                context.Response.StatusCode = 400;
                context.Response.Write("{\"result\": -1, \"message\": \"ERROR: El campo [DNI] no es válido, debe tener digitos sin guiones\"}");
                return;
            }

            if (!EsEmailValido(email)) {
                context.Response.StatusCode = 400;
                context.Response.Write("{\"result\": -1, \"message\": \"ERROR: El campo [Email] no es válido, dominio en el e-mail debe ser gmail.com ó hotmail.com\"}");
                return;
            }

            var responseJSON = serializer.Serialize(datosObj);
            context.Response.Write(responseJSON);
        }

        bool EsDNIValido(string dni) {
            if (dni.Length != 8) {
                return false;
            }
            foreach (char digito in dni) {
                if (!Char.IsDigit(digito)) {
                    return false;
                }
            }
            return true;
        }

        bool EsEmailValido(string email) {
            int indiceArroba = email.IndexOf("@");
            if (indiceArroba == -1)
                return false;
            string dominio = email.Substring(indiceArroba);
            if (!dominio.Equals("@hotmail.com", StringComparison.OrdinalIgnoreCase) && !dominio.Equals("@gmail.com", StringComparison.OrdinalIgnoreCase))
                return false;
            return true;
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}