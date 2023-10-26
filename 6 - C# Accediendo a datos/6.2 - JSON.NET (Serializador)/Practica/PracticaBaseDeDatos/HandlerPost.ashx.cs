using PracticaBaseDeDatos.Clases;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace PracticaBaseDeDatos {
    public class HandlerPost : IHttpHandler {
        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            string JSONReceived = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic datosObj = serializer.DeserializeObject(JSONReceived);
            try {
                Persona persona = new Persona(datosObj["nombre"], datosObj["apellido"], Convert.ToInt32(datosObj["edad"]), datosObj["dni"], datosObj["email"]);
                if (persona.Edad < 18) {
                    context.Response.Write(Constante.MENSAJE_ERROR_MENOR_DE_EDAD);
                    return;
                }
                string connectionStringVM = ConfigurationManager.AppSettings.Get("ConnectionStringVM").ToString();
                //string connectionStringLocal = ConfigurationManager.AppSettings.Get("ConnectionStringLocal").ToString();
                using (SqlConnection conexion = new SqlConnection(connectionStringVM)) {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(Constante.SP_AGREGAR_PERSONA, conexion)) {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_NOMBRE, persona.Nombre);
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_APELLIDO, persona.Apellido);
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_EDAD, persona.Edad);
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_DNI, persona.Dni);
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_EMAIL, persona.Email);
                        comando.ExecuteNonQuery();
                    }
                }
                context.Response.Write(Constante.MENSAJE_EXITOSO);
            }
            catch (DniInvalidoException) {
                context.Response.Write(Constante.MENSAJE_ERROR_DNI_INVALIDO);
                return;
            }
            catch (EmailInvalidoException) {
                context.Response.Write(Constante.MENSAJE_ERROR_EMAIL_INVALIDO);
                return;
            }
        }

        public bool IsReusable {
            get { return false; }
        }
    }
}