using API_Cinema.Entidades;
using API_Cinema.Framework.Common;
using System.Data.SqlClient;
using System.Data;

namespace API_Cinema.Datos.Implementaciones {
    public class DatosUsuario {

        private IConfiguration _configuration;
        private SqlConnection connection;

        public DatosUsuario(IConfiguration configuration) {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("CinemaDB"));
        }

        public List<Usuario> ObtenerUsuarios() {
            List<Usuario> registrosEncontrados = [];
            using (SqlCommand command = new(Constantes.SP_OBTENER_USUARIOS, connection)) {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new(command)) {
                    DataTable dt = new();
                    da.Fill(dt);
                    if (dt != null) {
                        Usuario? usuario = null;
                        foreach (DataRow dataRow in dt.Rows) {
                            usuario = new Usuario();
                            registrosEncontrados.Add(usuario.FromDataRow(dataRow));
                        }
                    }
                }
            }
            return registrosEncontrados;
        }

    }
}
