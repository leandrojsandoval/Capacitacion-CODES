using System.Configuration;
using System.Data.SqlClient;

namespace SampleAppConnection {
    internal class Program {

        static void Main() {
            // Para conectarnos a SQL Server utilizamos el objeto SqlConnection
            // Los datos de la conexion estan definidos en lo que comunmente se llama "ConnectionString"
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexion = new(connectionString)) {
                if (conexion.State != System.Data.ConnectionState.Open)
                    conexion.Open();
                SqlCommand comando = new("SELECT * FROM Provincias;", conexion);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read())
                        Console.WriteLine("{0}\t{1}", reader.GetInt32(0), reader.GetString(1));
                }
                else
                    Console.WriteLine("No hay filas encontradas");
                reader.Close();
            }
        }
    }
}