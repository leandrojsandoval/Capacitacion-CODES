using System.Web;

namespace SimpleAppWeb {
    public class test : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            string parametro1 = context.Request["parametro1"].ToString();
            string parametro2 = context.Request["parametro2"].ToString();
            string mensaje = string.Format("Hola Mundo !!! El parametro1 es {0} y el parametro2 es {1}",parametro1, parametro2);
            context.Response.Write(mensaje);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}