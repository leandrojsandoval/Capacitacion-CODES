using EjercicioIntegradorMVC_1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Linq;

namespace EjercicioIntegradorMVC_1.Controllers {
    public class PersonaController : Controller {

        public string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();

        /* Index: La vista de este metodo tiene botones a la opciones de: 
         * Listar, Agregar y Modificar */

        public ActionResult Index() {
            return View();
        }

        /* ListarPersonas: Obtiene la lista de personas de la base de datos y lo retorna para que se muestren
         * en la lista. */

        public ActionResult ListarPersonas() {
            List<Persona> personas = ObtenerPersonas();
            return View(personas);
        }

        /* AgregarPersona (GET): Se indica un formulario para completar en su vista, en donde el usuario ingrese
         * todos los campos requeridos. */

        public ActionResult AgregarPersona() {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarPersona(Persona persona) {
            if (ModelState.IsValid) {
                using (SqlConnection conexion = new SqlConnection(connectionString)) {
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

        /* ModificarPersona (GET): Muestra el formulario para editar una persona, pero antes de eso, se muestra
         * una lista despegable (select) en donde se encuentran todos los id's registrados en la base de datos
         * para indicar cual se quiere modificar, luego de eso, se autocompletan los campos con los datos actuales
         * que pueden ser modificables por el usuario. */

        public ActionResult ModificarPersona() {
            List<int> IdList = ObtenerListaIdPersonas();
            ViewBag.IdList = IdList.Select(id => new SelectListItem { Value = id.ToString(), Text = id.ToString() }).ToList();
            return View();
        }

        /* ObtenerPersonaPorID: Se invoca en la vista de ModificarPersona, la cual a traves de una lista 
         * despebable, el valor seleccionado (id) es el que llega por parametro y trae desde la base de datos 
         * los datos de la persona para mostrarlo en los campos del formulario. Antes de mostrarlo, es necesario
         * serializarlo. 
         * https://stackoverflow.com/questions/8464677/why-is-jsonrequestbehavior-needed
         * https://learn.microsoft.com/en-us/dotnet/api/system.web.mvc.jsonrequestbehavior?view=aspnet-mvc-5.2 */

        public ActionResult ObtenerPersonaPorID(int id) {
            Persona personaEncontrada = ObtenerPersonaPorId(id);
            if (personaEncontrada != null)
                return Json(personaEncontrada, JsonRequestBehavior.AllowGet);
            else
                return HttpNotFound();
        }

        /* ModificarPersona (POST): Una vez rellenado el formulario, los campos se agregar en un objeto de
         * clase Persona y esto es lo que se agrega a la base de datos.*/

        [HttpPost]
        public ActionResult ModificarPersona(Persona persona) {
            if (ModelState.IsValid) {
                using (SqlConnection conexion = new SqlConnection(connectionString)) {
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

        /* Exito: Si se agrego / modifico una persona correctamente, la vista de este metodo devuelve los datos que 
         * fueron agregados a la base de datos.*/

        public ActionResult Exito(Persona persona) {
            return View(persona);
        }

        /******************** Metodos privados utilizados en el controlador ********************/

        private List<Persona> ObtenerPersonas() {
            List<Persona> personas = new List<Persona>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Constante.SP_GET_PERSONAS, connection))
                using (SqlDataReader reader = command.ExecuteReader()) {
                    command.CommandType = CommandType.StoredProcedure;
                    while (reader.Read()) {
                        personas.Add(new Persona(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetInt32(3),
                            reader.GetString(4)
                        ));
                    }
                }
            }
            return personas;
        }

        private List<int> ObtenerListaIdPersonas() {
            List<int> idList = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Constante.SP_GET_IDS_PERSONAS, connection))
                using (SqlDataReader reader = command.ExecuteReader()) {
                    command.CommandType = CommandType.StoredProcedure;
                    while (reader.Read()) {
                        idList.Add(reader.GetInt32(0));
                    }
                }
            }
            return idList;
        }

        private int ObtenerUltimoId(SqlConnection conexion) {
            int idMax = Constante.ID_NO_ENCONTRADO;
            using (SqlCommand comando = new SqlCommand(Constante.SP_GET_MAX_ID_PERSONA, conexion)) {
                comando.CommandType = CommandType.StoredProcedure;
                var resultado = comando.ExecuteScalar();
                if (resultado != null) {
                    int.TryParse(resultado.ToString(), out idMax);
                }
            }
            return idMax;
        }

        private Persona ObtenerPersonaPorId(int id) {
            Persona persona = null;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Constante.SP_GET_PERSONA_POR_ID, connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(Constante.VARIABLE_ID, id));
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            persona = new Persona {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                TipoDoc = reader.GetInt32(3),
                                NroDoc = reader.GetString(4)
                            };
                        }
                    }
                }
            }
            return persona;
        }

        private bool InsertarPersonaEnBaseDeDatos(SqlConnection conexion, Persona persona) {
            try {
                using (SqlCommand comando = new SqlCommand(Constante.SP_INSERTAR_PERSONA, conexion)) {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue(Constante.VARIABLE_NOMBRE, persona.Nombre);
                    comando.Parameters.AddWithValue(Constante.VARIABLE_APELLIDO, persona.Apellido);
                    comando.Parameters.AddWithValue(Constante.VARIABLE_TIPO_DOCUMENTO, persona.TipoDoc);
                    comando.Parameters.AddWithValue(Constante.VARIABLE_NUMERO_DOCUMENTO, persona.NroDoc);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex) {
                throw new InsercionInvalidaException(Constante.MENSAJE_ERROR_INSERCION, ex);
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException(Constante.MENSAJE_ERROR_ARGUMENTOS_NULOS, ex);
            }
        }

        private bool ModificarPersonaEnBaseDeDatos(SqlConnection conexion, Persona persona) {
            try {
                using (SqlCommand comando = new SqlCommand(Constante.SP_ACTUALIZAR_PERSONA, conexion)) {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue(Constante.VARIABLE_ID, persona.Id);
                    comando.Parameters.AddWithValue(Constante.VARIABLE_NOMBRE, persona.Nombre);
                    comando.Parameters.AddWithValue(Constante.VARIABLE_APELLIDO, persona.Apellido);
                    comando.Parameters.AddWithValue(Constante.VARIABLE_TIPO_DOCUMENTO, persona.TipoDoc);
                    comando.Parameters.AddWithValue(Constante.VARIABLE_NUMERO_DOCUMENTO, persona.NroDoc);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex) {
                throw new InsercionInvalidaException(Constante.MENSAJE_ERROR_ACTUALIZACION, ex);
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException(Constante.MENSAJE_ERROR_ARGUMENTOS_NULOS, ex);
            }
        }

    }
}