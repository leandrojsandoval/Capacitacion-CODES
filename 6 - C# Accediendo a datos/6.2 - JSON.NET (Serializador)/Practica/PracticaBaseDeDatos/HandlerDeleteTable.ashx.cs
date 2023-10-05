using System.Data.SqlClient;
using System.Web;

namespace PracticaBaseDeDatos {
    public class HandlerDeleteTable : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";

            // Aca se deberia cambiar por el servidor de la VM
            var datosBD = "Server=DESKTOP-7EJ9QTF\\MSSQLSERVER01;"
            + "Database=DBPracticaCODES;"
            + "Integrated Security=True;";

            using (SqlConnection conexion = new SqlConnection(datosBD)) {
                conexion.Open();
                var comandoEliminacion = "DELETE Persona";
                using (SqlCommand comando = new SqlCommand(comandoEliminacion, conexion)) {
                    context.Response.Write(comando.ExecuteNonQuery() > 0 ? "La tabla se borró exitosamente." : "La tabla no se borró o estaba vacía.");
                }
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}