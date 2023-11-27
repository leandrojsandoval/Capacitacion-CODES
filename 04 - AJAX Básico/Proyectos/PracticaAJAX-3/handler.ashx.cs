using System;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace PracticaAJAX_3 {
    public class Handler : IHttpHandler {

        public const string DOMINIO_GMAIL = "@gmail.com";
        public const string DOMINIO_HOTMAIL = "@hotmail.com";
        public const string MENSAJE_ERROR_EDAD = "ERROR: El campo [Edad] no es válido, debe tener al menos 18 años";
        public const string MENSAJE_ERROR_DNI = "ERROR: El campo [DNI] no es válido, debe tener digitos sin guiones";
        public const string MENSAJE_ERROR_EMAIL = "ERROR: El campo [Email] no es válido, dominio en el e-mail debe ser gmail.com ó hotmail.com";
        public const int RESULTADO_VALIDO = 0;
        public const int RESULTADO_INVALIDO = -1;

        public void ProcessRequest(HttpContext context) {
            
            context.Response.ContentType = "text/plain";
            string JSONReceived = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic datosObj = serializer.DeserializeObject(JSONReceived);

            int edad = Convert.ToInt32(datosObj["edad"]);
            string dni = datosObj["dni"];
            string email = datosObj["email"];

            if (edad < 18) {
                context.Response.Write("{\"result\":" + RESULTADO_INVALIDO + ", \"message\": \"" + MENSAJE_ERROR_EDAD + "\"}");
                return;
            }

            if (!EsDNIValido(dni)) {
                context.Response.Write("{\"result\":" + RESULTADO_INVALIDO + ", \"message\": \"" + MENSAJE_ERROR_DNI + "\"}");
                return;
            }

            if (!EsEmailValido(email)) {
                context.Response.Write("{\"result\": -1, \"message\": \"" + MENSAJE_ERROR_EMAIL + "\"}");
                return;
            }

            var responseJSON = serializer.Serialize(datosObj);
            context.Response.Write(responseJSON);
        }

        bool EsDNIValido(string dni) {
            if (dni.Length != 8)
                return false;
            foreach (char digito in dni) {
                if (!char.IsDigit(digito))
                    return false;
            }
            return true;
        }

        bool EsEmailValido(string email) {
            // Si tiene arroba o no, esta validado en la parte de JavaScript
            int indiceArroba = email.IndexOf("@");
            string dominio = email.Substring(indiceArroba);
            return dominio.Equals(DOMINIO_HOTMAIL, StringComparison.OrdinalIgnoreCase) || dominio.Equals(DOMINIO_GMAIL, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}