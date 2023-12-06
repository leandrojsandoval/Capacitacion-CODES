using API_Cinema.Entidades;
using API_Cinema.Framework.Common;
using System.Data;
using System.Data.SqlClient;

namespace API_Cinema.Datos.Implementaciones {
    public class DatosHorario {

        private IConfiguration _configuration;
        private SqlConnection connection;

        public DatosHorario(IConfiguration configuration) {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("CinemaDB"));
        }

        public void Insertar(Horario horario) {

            using (SqlCommand command = new SqlCommand(Constantes.SP_HORARIO_INSERT, connection)) {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idSucursal", horario.IdSucursal);
                command.Parameters.AddWithValue("@idPelicula", horario.IdPelicula);
                command.Parameters.AddWithValue("@hora", horario.Hora);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
