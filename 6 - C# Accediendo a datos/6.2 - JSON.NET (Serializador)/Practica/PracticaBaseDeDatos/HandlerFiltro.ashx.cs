using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace PracticaBaseDeDatos {
    public class HandlerFiltro : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";

            string columna = context.Request["columna"];
            string filtro = context.Request["filtro"];

            List<Persona> resultados = new List<Persona>();

            // Aca se debería cambiar por el servidor de la VM
            var datosBD = "Server=DESKTOP-7EJ9QTF\\MSSQLSERVER01;"
                        + "Database=DBPracticaCODES;"
                        + "Integrated Security=True;";

            using (SqlConnection conexion = new SqlConnection(datosBD)) {
                
                conexion.Open();
                var comandoSeleccion = "SELECT * FROM Persona WHERE " + columna + " LIKE @filtro";
                
                using (SqlCommand comando = new SqlCommand(comandoSeleccion, conexion)) {
                    
                    comando.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    SqlDataReader reader = comando.ExecuteReader();
                    
                    while (reader.Read()) {

                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellido"].ToString();
                        int edad = Convert.ToInt32(reader["Edad"]);
                        string dni = reader["Dni"].ToString();
                        string email = reader["Email"].ToString();

                        resultados.Add(new Persona(nombre, apellido, edad, dni, email));
                    }
                }
            }

            string json = JsonConvert.SerializeObject(resultados);
            context.Response.Write(json);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}