using System;
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

            string responseJSON;

            try {
                Persona persona = new Persona(datosObj["nombre"], datosObj["apellido"], Convert.ToInt32(datosObj["edad"]), datosObj["dni"], datosObj["email"]);

                if (persona.Edad < 18) {
                    responseJSON = serializer.Serialize(new { result = -1, message = "ERROR: El campo [Edad] no es válido, debe tener al menos 18 años" });
                    context.Response.Write(responseJSON);
                    return;
                }

                // Aca se deberia cambiar por el servidor de la VM
                var datosBD = "Server=DESKTOP-7EJ9QTF\\MSSQLSERVER01;"
                            + "Database=DBPracticaCODES;"
                            + "Integrated Security=True;";

                using (SqlConnection conexion = new SqlConnection(datosBD)) {
                    conexion.Open();
                    var comandoInsercion = "INSERT INTO Persona (Nombre, Apellido, Edad, DNI, Email) VALUES (@Nombre, @Apellido, @Edad, @DNI, @Email)";
                    using (SqlCommand comando = new SqlCommand(comandoInsercion, conexion)) {
                        comando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                        comando.Parameters.AddWithValue("@Apellido", persona.Apellido);
                        comando.Parameters.AddWithValue("@Edad", persona.Edad);
                        comando.Parameters.AddWithValue("@DNI", persona.Dni);
                        comando.Parameters.AddWithValue("@Email", persona.Email);

                        comando.ExecuteNonQuery();
                    }
                }

                responseJSON = serializer.Serialize(new { result = 0, message = " " });
                context.Response.Write(responseJSON);
            }
            catch (DniInvalidoException) {
                responseJSON = serializer.Serialize(new { result = -1, message = "ERROR: El campo [DNI] no es válido, debe tener digitos sin guiones" });
                context.Response.Write(responseJSON);
                return;
            }
            catch (EmailInvalidoException) {
                responseJSON = serializer.Serialize(new { result = -1, message = "ERROR: El campo [Email] no es válido, dominio en el e-mail debe ser gmail.com ó hotmail.com" });
                context.Response.Write(responseJSON);
                return;
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}