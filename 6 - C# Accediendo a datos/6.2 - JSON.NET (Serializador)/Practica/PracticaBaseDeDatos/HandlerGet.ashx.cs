using Newtonsoft.Json;
using PracticaBaseDeDatos.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace PracticaBaseDeDatos {
    public class HandlerGet : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            //context.Response.ContentType = "application/json";
            List<Persona> listaPersonas = new List<Persona>();
            try {
                string connectionStringVM = ConfigurationManager.AppSettings.Get("ConnectionStringVM").ToString();
                string connectionStringLocal = ConfigurationManager.AppSettings.Get("ConnectionStringLocal").ToString();
                using (SqlConnection conexion = new SqlConnection(connectionStringLocal)) {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(Constante.SP_OBTENER_PERSONAS, conexion)) {
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read()) {
                            listaPersonas.Add(new Persona {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Edad = Convert.ToInt32(reader["Edad"]),
                                Dni = reader["Dni"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
                string json = JsonConvert.SerializeObject(listaPersonas);
                context.Response.Write(json);
            }
            catch (Exception ex) {
                context.Response.StatusCode = 500;
                context.Response.Write("Error: " + ex.Message);
            }
        }

        public bool IsReusable {
            get { return false; }
        }
    }
}