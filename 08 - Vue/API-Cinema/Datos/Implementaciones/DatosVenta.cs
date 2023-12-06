using API_Cinema.Entidades;
using API_Cinema.Framework.Common;
using System.Data.SqlClient;
using System.Data;

namespace API_Cinema.Datos.Implementaciones {
    public class DatosVenta {

        private IConfiguration _configuration;
        private SqlConnection connection;

        public DatosVenta(IConfiguration configuration) {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("CinemaDB"));
        }

        public List<Venta> ObtenerVentas() {
            List<Venta> registrosEncontrados = [];
            using (SqlCommand command = new(Constantes.SP_OBTENER_VENTAS, connection)) {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new(command)) {
                    DataTable dt = new();
                    da.Fill(dt);
                    if (dt != null) {
                        Venta? venta = null;
                        foreach (DataRow dataRow in dt.Rows) {
                            venta = new Venta();
                            registrosEncontrados.Add(venta.FromDataRow(dataRow));
                        }
                    }
                }
            }
            return registrosEncontrados;
        }

        public void Insertar(Venta venta) {

            using (SqlCommand command = new SqlCommand(Constantes.SP_VENTA_INSERT, connection)) {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", venta.IdUsuario);
                command.Parameters.AddWithValue("@idHorario", venta.IdHorario);
                command.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                command.Parameters.AddWithValue("@total", venta.Total);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}
