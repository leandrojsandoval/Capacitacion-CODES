using Newtonsoft.Json;
using PracticaBaseDeDatos.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace PracticaBaseDeDatos {
    public class HandlerFiltro : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            //context.Response.ContentType = "application/json";

            string nombre = context.Request["nombre"];
            string apellido = context.Request["apellido"];
            string dni = context.Request["dni"];
            string email = context.Request["email"];
            string edad = context.Request["edad"];

            List<Persona> listaPersonas = new List<Persona>();

            string connectionStringVM = ConfigurationManager.AppSettings.Get("ConnectionStringVM").ToString();
            string connectionStringLocal = ConfigurationManager.AppSettings.Get("ConnectionStringLocal").ToString();

            using (SqlConnection conexion = new SqlConnection(connectionStringVM)) {

                conexion.Open();

                using (SqlCommand comando = new SqlCommand(Constante.SP_FILTRAR_PERSONAS, conexion)) {

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue(Constante.PARAMETRO_NOMBRE, nombre);
                    comando.Parameters.AddWithValue(Constante.PARAMETRO_APELLIDO, apellido);
                    comando.Parameters.AddWithValue(Constante.PARAMETRO_DNI, dni);
                    comando.Parameters.AddWithValue(Constante.PARAMETRO_EMAIL, email);
                    comando.Parameters.AddWithValue(Constante.PARAMETRO_EDAD, edad);

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

        public bool IsReusable {
            get { return false; }
        }
    }
}