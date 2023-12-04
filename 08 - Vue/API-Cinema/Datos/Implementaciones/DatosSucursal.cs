using API_Cinema.Entidades;
using API_Cinema.Entidades.Filtros;
using API_Cinema.Framework.Common;
using System.Data;
using System.Data.SqlClient;

namespace API_Cinema.Datos.Implementaciones {
    public class DatosSucursal {

        private IConfiguration _configuration;
        private SqlConnection connection;
        
        public DatosSucursal(IConfiguration configuration) {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("CinemaDB"));
        }
        
        public void Insertar(Sucursal sucursal) {

            using (SqlCommand command = new SqlCommand(Constantes.SP_SUCURSAL_INSERT, connection)) {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombre", sucursal.Nombre);
                command.Parameters.AddWithValue("@precio", sucursal.Precio);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        
        public void Actualizar(Sucursal sucursal) {

            using (SqlCommand command = new SqlCommand(Constantes.SP_SUCURSAL_ACTUALIZAR, connection)) {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("id", sucursal.Id);
                command.Parameters.AddWithValue("nombre", sucursal.Nombre);
                command.Parameters.AddWithValue("precio", sucursal.Precio);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        
        public List<Sucursal> ObtenerSucursales() {
            List<Sucursal> registrosEncontrados = new();
            using (SqlCommand command = new(Constantes.SP_OBTENER_SUCURSALES, connection)) {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new(command)) {
                    DataTable dt = new();
                    da.Fill(dt);
                    if (dt != null) {
                        Sucursal? sucursal = null;
                        foreach (DataRow dataRow in dt.Rows) {
                            sucursal = new Sucursal();
                            registrosEncontrados.Add(sucursal.FromDataRow(dataRow));
                        }
                    }
                }
            }
            return registrosEncontrados;
        }
        
        public List<Sucursal> ObtenerSucursalPorId(int id) {
            List<Sucursal> registrosEncontrados = new();
            using (SqlCommand command = new(Constantes.SP_OBTENER_SUCURSAL_POR_ID, connection)) {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter da = new(command)) {
                    DataTable dt = new();
                    da.Fill(dt);
                    if (dt != null) {
                        Sucursal? sucursal = null;
                        foreach (DataRow dataRow in dt.Rows) {
                            sucursal = new Sucursal();
                            registrosEncontrados.Add(sucursal.FromDataRow(dataRow));
                        }
                    }
                }
            }
            return registrosEncontrados;
        }

    }
}
