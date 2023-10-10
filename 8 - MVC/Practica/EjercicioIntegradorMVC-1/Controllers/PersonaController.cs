using EjercicioIntegradorMVC_1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace EjercicioIntegradorMVC_1.Controllers {
    public class PersonaController : Controller {

        public const int ID_NO_ENCONTRADO = -1;

        public ActionResult Index() {
            return View();
        }

        public ActionResult ListarPersonas() {
            List<Persona> personas = ObtenerPersonas();
            return View(personas);
        }

        [HttpPost]
        public ActionResult AgregarPersona(Persona persona) {
            if (ModelState.IsValid) {
                string conexionCadena = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
                using (SqlConnection conexion = new SqlConnection(conexionCadena)) {
                    try {
                        conexion.Open();
                        bool insercionExitosa = InsertarPersonaEnBaseDeDatos(conexion, persona);
                        if (insercionExitosa) {
                            persona.Id = ObtenerUltimoId(conexion);
                            return RedirectToAction("Exito", persona);
                        }
                        else
                            ModelState.AddModelError(string.Empty, "No se pudo agregar la persona.");
                    }
                    catch (SqlException exception) {
                        ModelState.AddModelError(string.Empty, "Ocurrio una excepcion al agregar una persona: " + exception.Message);
                    }
                }
            }
            return View(persona);
        }

        [HttpPost]
        public ActionResult ModificarPersona(Persona persona) {
            if (ModelState.IsValid) {
                string conexionCadena = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
                using (SqlConnection conexion = new SqlConnection(conexionCadena)) {
                    try {
                        conexion.Open();
                        bool modificacionExitosa = ModificarPersonaEnBaseDeDatos(conexion, persona);
                        if (modificacionExitosa)
                            return RedirectToAction("Exito", persona);
                        else
                            ModelState.AddModelError(string.Empty, "No se pudo modificar la persona.");
                    }
                    catch (SqlException exception) {
                        ModelState.AddModelError(string.Empty, "Ocurrio una excepcion al modificar una persona: " + exception.Message);
                    }
                }
            }
            return View(persona);
        }

        public ActionResult Exito(Persona persona) {
            return View(persona);
        }

        // Metodos utilizados en los controladores

        private bool ModificarPersonaEnBaseDeDatos(SqlConnection conexion, Persona persona) {
            try {
                string consulta = "UPDATE Persona " +
                    "SET Nombre = @Nombre, Apellido = @Apellido, TipoDoc = @TipoDoc, NroDoc = @NroDoc " +
                    "WHERE Id = @Id;";
                using (SqlCommand comando = new SqlCommand(consulta, conexion)) {
                    comando.Parameters.AddWithValue("@Id", persona.Id);
                    comando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", persona.Apellido);
                    comando.Parameters.AddWithValue("@TipoDoc", persona.TipoDoc);
                    comando.Parameters.AddWithValue("@NroDoc", persona.NroDoc);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException) {
                throw new InsercionInvalidaException("Error al insertar la persona en la base de datos.");
            }
        }

        private int ObtenerUltimoId(SqlConnection conexion) {
            int id = ID_NO_ENCONTRADO;
            string consulta = "SELECT MAX(Id) FROM Persona";
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            using (SqlDataReader reader = comando.ExecuteReader()) {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }
            return id;
        }

        private bool InsertarPersonaEnBaseDeDatos(SqlConnection conexion, Persona persona) {
            try {
                string consulta = "INSERT INTO Persona (Nombre, Apellido, TipoDoc, NroDoc) VALUES (@Nombre, @Apellido, @TipoDoc, @NroDoc);";
                using (SqlCommand comando = new SqlCommand(consulta, conexion)) {
                    comando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", persona.Apellido);
                    comando.Parameters.AddWithValue("@TipoDoc", persona.TipoDoc);
                    comando.Parameters.AddWithValue("@NroDoc", persona.NroDoc);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException) {
                throw new InsercionInvalidaException("Error al insertar la persona en la base de datos.");
            }
        }

        private List<Persona> ObtenerPersonas() {
            List<Persona> personas = new List<Persona>();
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                string query = "SELECT * FROM Persona;";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {

                        int id = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        string apellido = reader.GetString(2);
                        int tipoDoc = reader.GetInt32(3);
                        string nroDoc = reader.GetString(4);

                        personas.Add(new Persona(id, nombre, apellido, tipoDoc, nroDoc));
                    }
                }
            }
            return personas;
        }
    
    }
}