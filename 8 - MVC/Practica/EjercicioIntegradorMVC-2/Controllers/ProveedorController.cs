using EjercicioIntegradorMVC_2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace EjercicioIntegradorMVC_2.Controllers {
    public class ProveedorController : Controller {

        private readonly List<Provincia> provincias;
        private readonly List<Localidad> localidades;

        public string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();

        public ProveedorController() {

            provincias = ObtenerProvincias();
            localidades = ObtenerLocalidades();

            /* Paso estas listas a la vista con ViewBag para poder invocarlas en las vistas que quiera
             * estas vista se utilizan en Indice y cada vez que quiero buscar por algun filtro*/

            ViewBag.Provincias = provincias;
            ViewBag.Localidades = localidades;

            /* Tambien las guardo como listas despegables para cuando quiera entrar a la vista Edit.
             * Esto es necesario para luego mostrar en el formulario la lista despegable de
             * las provincias y localidades que posee la base de datos disponibles para
             * que el usuario seleccione */

            ViewBag.ProvinciasSelect = new SelectList(provincias, "Id", "Descripcion");
            ViewBag.LocalidadesSelect = new SelectList(localidades, "Id", "Descripcion");
        }

        /* Indice: Muestra la tabla de la base de datos de los Proveedores.
         * Es necesario que el constructor obtenga las otras tablas de Provincia 
         * y Localidad para mostrar el nombre, ya que en Proveedor solo esta el ID*/

        public ActionResult Indice() {
            /* Paso la lista de proveedores con @model*/
            return View(ObtenerProveedores());
        }

        /* FiltrarProveedores: Segun los criterios, se puede buscar unicamente los proveedores que
         * el usuario desee en base a los tres parametros que le llegan al controlador. 
         * No hay problema con que se pasen vacio los campos ya que eso esta cubierto en la consulta. 
         * (Ver la constante CONSULTA_FILTRO_PROVEEDOR)*/

        [HttpPost]
        public ActionResult FiltrarProveedores(string nombre, string provincia, string localidad) {

            try {

                using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {

                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(Constante.SP_FILTRAR_PROVEEDOR, conexion)) {

                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_NOMBRE, nombre);
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_PROVINCIA, provincia);
                        comando.Parameters.AddWithValue(Constante.PARAMETRO_LOCALIDAD, localidad);

                        List<Proveedor> proveedoresFiltrados = new List<Proveedor>();

                        using (SqlDataReader reader = comando.ExecuteReader()) {

                            while (reader.Read()) {

                                int id = reader.GetInt32(0);
                                string proveedorNombre = reader.GetString(1);
                                string domicilio = reader.GetString(2);
                                int proveedorIdProvincia = reader.GetInt32(3);
                                int proveedorIdLocalidad = reader.GetInt32(4);
                                proveedoresFiltrados.Add(new Proveedor(id, proveedorNombre, domicilio, proveedorIdProvincia, proveedorIdLocalidad));

                            }

                        }

                        return View(Constante.VISTA_INDICE, proveedoresFiltrados);
                    }
                }
            }
            catch (SqlException sqlEx) {
                ViewBag.ErrorMessage = "Error en la base de datos: " + sqlEx.Message;
                return View(Constante.VISTA_INDICE);
            }
            catch (Exception ex) {
                ViewBag.ErrorMessage = "Ocurrió un error al filtrar los proveedores: " + ex.Message;
                return View(Constante.VISTA_INDICE);
            }
        }

        /* Details(int id): Muestra los detalles del proveedor en el formulario del a vista Edit */

        public ActionResult Details(int id) {

            Proveedor proveedorEncontrado = ObtenerProveedorPorId(id);

            /* Agrego el control, aunque proveedorEncontrado no puede dar null ya que el ID proviene de 
             * la lista de proveedores ya cargada en la base de datos, a no ser que a esta plataforma 
             * puedan ingresar mas de un usario y justo en el momento que se accede a los detalles al 
             * mismo tiempo otro usuario lo elimino */

            if (proveedorEncontrado == null) {
                return HttpNotFound();
            }

            return View("Edit", proveedorEncontrado);

        }

        /* Edit (GET): Muestro el formulario con los campos en blanco para que el usuario complete, 
         * con el proposito de agregar un proveedor nuevo a la base de datos. */

        public ActionResult Edit() {
            return View();
        }

        /* Edit (POST): Cuando se termina de completar el formulario. Este controlador 
         * maneja el objeto para luego insertarlo en la base de datos */

        [HttpPost]
        public ActionResult Edit(Proveedor proveedor) {
            if (ModelState.IsValid) {
                try {
                    string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
                    using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                        conexion.Open();
                        using (SqlCommand comando = new SqlCommand(Constante.SP_AGREGAR_PROVEEDOR, conexion)) {
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.Parameters.AddWithValue(Constante.PARAMETRO_NOMBRE, proveedor.Nombre);
                            comando.Parameters.AddWithValue(Constante.PARAMETRO_DOMICILIO, proveedor.Domicilio);
                            comando.Parameters.AddWithValue(Constante.PARAMETRO_ID_PROVINCIA, proveedor.IdProvincia);
                            comando.Parameters.AddWithValue(Constante.PARAMETRO_ID_LOCALIDAD, proveedor.IdLocalidad);
                            if (comando.ExecuteNonQuery() > 0)
                                return RedirectToAction(Constante.VISTA_INDICE);
                            else
                                ModelState.AddModelError(string.Empty, "Error al agregar el proveedor.");
                        }
                    }
                }
                catch (SqlException sqlEx) {
                    ModelState.AddModelError(string.Empty, "Error de base de datos: " + sqlEx.Message);
                }
                catch (Exception ex) {
                    ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
                }
            }
            return View(proveedor);
        }

        /* EliminarProveedor: Gracias al ID que se tiene al mostrar la lista, se puede eliminar
         * dicho proveedor.*/

        public ActionResult EliminarProveedor(int id) {
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(Constante.SP_ELIMINAR_PROVEEDOR, conexion)) {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue(Constante.PARAMETRO_ID, id);
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction(Constante.VISTA_INDICE);
        }

        /* ObtenerLocalidadesPorProvincia: Este controlador trae solo las localidades
         * pertenecientes a la provincia que el usuario selecciono. */

        [HttpPost]
        public ActionResult ObtenerLocalidadesPorProvincia(int idProvincia) {
            var localidades = ObtenerLocalidadesPorIdDeProvincia(idProvincia);
            /* El meotodo Json ayuda a la serializacion de objetos .NET para 
             convertirlos en formato JSON.
            
            JsonRequestBehavior.AllowGet indica que las solicitudes JSON son permitidas
            del lado del cliente hacia el controlador, estos datos son recibidos y 
            procesados por el servidor*/
            return Json(localidades, JsonRequestBehavior.AllowGet);
        }

        /********************************************************************************************/

        private List<Localidad> ObtenerLocalidades() {
            List<Localidad> localidades = null;
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            try {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(Constante.SP_OBTENER_LOCALIDADES, conexion)) {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = comando.ExecuteReader()) {
                            localidades = new List<Localidad>();
                            while (reader.Read()) {
                                int id = reader.GetInt32(0);
                                string descripcion = reader.GetString(1);
                                int idProvincia = reader.GetInt32(2);
                                localidades.Add(new Localidad(id, descripcion, idProvincia));
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx) {
                ModelState.AddModelError(string.Empty, "Error de base de datos: " + sqlEx.Message);
            }
            return localidades;
        }

        private List<Localidad> ObtenerLocalidadesPorIdDeProvincia(int idProvincia) {
            if (idProvincia < 1)
                throw new IdInvalidoException("El ID enviado por parametro es invalido.");
            List<Localidad> localidades = ObtenerLocalidades();
            List<Localidad> localidadesFiltradas = localidades.Where(localidad => localidad.IdProvincia == idProvincia).ToList();
            return localidadesFiltradas;
        }

        private List<Provincia> ObtenerProvincias() {
            List<Provincia> provincias = null;
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            try {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(Constante.SP_OBTENER_PROVINCIAS, conexion)) {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = comando.ExecuteReader()) {
                            provincias = new List<Provincia>();
                            while (reader.Read()) {
                                int id = reader.GetInt32(0);
                                string descripcion = reader.GetString(1);
                                provincias.Add(new Provincia(id, descripcion));
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx) {
                ModelState.AddModelError(string.Empty, "Error de base de datos: " + sqlEx.Message);
            }
            return provincias;
        }

        private List<Proveedor> ObtenerProveedores() {
            List<Proveedor> proveedores = null;
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            try {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(Constante.SP_OBTENER_PROVEEDORES, conexion)) {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = comando.ExecuteReader()) {
                            proveedores = new List<Proveedor>();
                            while (reader.Read()) {
                                int id = reader.GetInt32(0);
                                string nombre = reader.GetString(1);
                                string domicilio = reader.GetString(2);
                                int idProvincia = reader.GetInt32(3);
                                int idLocalidad = reader.GetInt32(4);
                                proveedores.Add(new Proveedor(id, nombre, domicilio, idProvincia, idLocalidad));
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx) {
                ModelState.AddModelError(string.Empty, "Error de base de datos: " + sqlEx.Message);
            }
            return proveedores;
        }

        private Proveedor ObtenerProveedorPorId(int id) {
            if (id < 1)
                throw new IdInvalidoException("El ID no corresponde a un ID válido");
            List<Proveedor> proveedores = ObtenerProveedores();
            return proveedores.FirstOrDefault(proveedor => proveedor.Id == id);
        }

    }
}
