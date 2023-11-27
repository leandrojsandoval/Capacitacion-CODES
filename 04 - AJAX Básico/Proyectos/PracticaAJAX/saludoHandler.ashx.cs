using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaAJAX {
    /// <summary>
    /// Summary description for saludoHandler
    /// </summary>
    public class saludoHandler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            string nombre = context.Request["nombre"].ToString();
            string apellido = context.Request["apellido"].ToString();
            string mensaje = string.Format("Hola {0} {1}!!!", nombre, apellido);
            context.Response.Write(mensaje);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}