using System.Data.SqlClient;

namespace ProyectoAccesoALaBD {
    internal class Program {
        static void Main() {
            // Referencias rapidas de diferentes formatos de ConnectionStrings
            // https://www.connectionstrings.com/sql-server-2005/

            // 1 - Configuramos los parametros
            var connectionString =
                "Server=SVR-SQLCAP01\\SQLEXPRESS;" +
                "Database=CAP_Practica_2;" +
                "User Id=sa;" +
                "Password=codes;";

            try {
                // 2- Nos conectamos utilizando SqlConnection
                using SqlConnection connection = new(connectionString);
                connection.Open();

                // 3 - Ejecutamos la consulta
                var comando = "SELECT * FROM Familia ORDER BY fami_detalle ASC";
                using SqlCommand sqlcom = new(comando, connection);

                // 4 - Si llegamos hasta aca, significa que nos conectamos y la consulta no dio ningun error
                // Obtenemos los datos del comando utilizando SqlDataReader
                SqlDataReader reader = sqlcom.ExecuteReader();
                while (reader.Read()) {
                    Console.WriteLine("Id: " + reader[0] + ";\tDetalle: " + reader[1]);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}