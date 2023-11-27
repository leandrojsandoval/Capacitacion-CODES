using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaAJAX_1 {
    /// <summary>
    /// Summary description for myHandler
    /// </summary>
    public class myHandler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            string categoria = context.Request["categoria"];
            switch(categoria) {
                case "frutas":
                    context.Response.Write("<select>" +
                        "<option>Manzana</option>" +
                        "<option>Pera</option>" +
                        "</select>");
                    break;
                case "verduras":
                    context.Response.Write("<select>" + 
                        "<option>Papa</option>" +
                        "<option>Lechuga</option>" +
                        "</select>");
                    break;
                default:
                    context.Response.Write("Categoria invalida");
                    break;
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}