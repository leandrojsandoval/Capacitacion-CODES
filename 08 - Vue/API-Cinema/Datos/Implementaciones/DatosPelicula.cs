using API_Cinema.Entidades;
using API_Cinema.Framework.Common;
using System.Data;
using System.Data.SqlClient;

namespace API_Cinema.Datos.Implementaciones {
    public class DatosPelicula {

        private IConfiguration _configuration;
        private SqlConnection connection;

        public DatosPelicula(IConfiguration configuration) {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("CinemaDB"));
        }

        public List<Pelicula> ObtenerPeliculas() {
            List<Pelicula> registrosEncontrados = [];
            using (SqlCommand command = new(Constantes.SP_OBTENER_PELICULAS, connection)) {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new(command)) {
                    DataTable dt = new();
                    da.Fill(dt);
                    if (dt != null) {
                        Pelicula? pelicula = null;
                        foreach (DataRow dataRow in dt.Rows) {
                            pelicula = new Pelicula();
                            registrosEncontrados.Add(pelicula.FromDataRow(dataRow));
                        }
                    }
                }
            }
            return registrosEncontrados;
        }

        public Pelicula ObtenerPeliculaPorId(int id) {
            Pelicula pelicula = null;
            using (SqlCommand command = new(Constantes.SP_OBTENER_PELICULA_POR_ID, connection)) {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        pelicula = new Pelicula().FromDataReader(reader);
                    }
                }
            }
            return pelicula;
        }

    }

}
