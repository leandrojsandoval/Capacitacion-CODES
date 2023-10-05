using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace PracticaBaseDeDatos {
    public class HandlerGet : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";

            List<Persona> listaPersonas = new List<Persona>();

            // Aca se deberia cambiar por el servidor de la VM
            var datosBD = "Server=DESKTOP-7EJ9QTF\\MSSQLSERVER01;"
                        + "Database=DBPracticaCODES;"
                        + "Integrated Security=True;";

            using (SqlConnection conexion = new SqlConnection(datosBD)) {
                conexion.Open();
                var comandoSeleccion = "SELECT * FROM Persona";
                using (SqlCommand comando = new SqlCommand(comandoSeleccion, conexion)) {
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read()) {

                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellido"].ToString();
                        int edad = Convert.ToInt32(reader["Edad"]);
                        string dni = reader["Dni"].ToString();
                        string email = reader["Email"].ToString();

                        listaPersonas.Add(new Persona(nombre, apellido, edad, dni, email));
                    }
                }
            }

            string json = JsonConvert.SerializeObject(listaPersonas);
            context.Response.Write(json);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}