using PracticaBaseDeDatos.Clases;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace PracticaBaseDeDatos {
    public class HandlerDeleteTable : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexion = new SqlConnection(connectionString)) {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(Constante.SP_ELIMINAR_PERSONAS, conexion)) {
                    comando.CommandType = CommandType.StoredProcedure;
                    context.Response.Write(comando.ExecuteNonQuery() > 0 ? Constante.MENSAJE_BORRADO_EXITOSO : Constante.MENSAJE_BORRADO_FALLIDO);
                }
            }
        }

        public bool IsReusable {
            get { return false; }
        }
    }
}