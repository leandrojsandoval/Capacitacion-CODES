using EjercicioIntegradorMVC_2_VM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjercicioIntegradorMVC_2_VM.Controllers {
    public class ProveedorController : Controller {
        // GET: Proveedor
        public ActionResult Indice () {
            List<Provincia> provincias = ObtenerProvincias();
            List<Localidad> localidades = ObtenerLocalidades();

            ViewData["Provincias"] = provincias;
            ViewData["Localidades"] = localidades;

            return View(ObtenerProveedores());
        }

        [HttpPost]
        public ActionResult Edit (Proveedor proveedor) {
            try {
                string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
                using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                    conexion.Open();
                    string consulta = "INSERT INTO Proveedor (Nombre, Domicilio, IdProvincia, IdLocalidad) VALUES (@Nombre, @Domicilio, @IdProvincia, @IdLocalidad);";
                    using (SqlCommand comando = new SqlCommand(consulta, conexion)) {
                        comando.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                        comando.Parameters.AddWithValue("@Domicilio", proveedor.Domicilio);
                        comando.Parameters.AddWithValue("@IdProvincia", proveedor.IdProvincia);
                        comando.Parameters.AddWithValue("@IdLocalidad", proveedor.IdLocalidad);
                        comando.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Indice");
            }
            catch {
                return View();
            }
        }

        public ActionResult EliminarProveedor (int id) {
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                conexion.Open();
                string consulta = "DELETE FROM Proveedor WHERE Id = @Id;";
                using (SqlCommand comando = new SqlCommand(consulta, conexion)) {
                    comando.Parameters.AddWithValue("@Id", id);
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Indice");
        }

        private List<Localidad> ObtenerLocalidades () {
            List<Localidad> localidades = new List<Localidad>();
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                conexion.Open();
                string consulta = "SELECT * FROM Localidad;";
                using (SqlCommand comando = new SqlCommand(consulta, conexion)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        while (reader.Read()) {
                            int id = reader.GetInt32(0);
                            string descripcion = reader.GetString(1);
                            int idProvincia = reader.GetInt32(2);
                            localidades.Add(new Localidad(id, descripcion, idProvincia));
                        }
                    }
                }
            }
            return localidades;
        }

        private List<Provincia> ObtenerProvincias () {
            List<Provincia> provincias = new List<Provincia>();
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                conexion.Open();
                string consulta = "SELECT * FROM Provincia;";
                using (SqlCommand comando = new SqlCommand(consulta, conexion)) {
                    using(SqlDataReader reader = comando.ExecuteReader()) {
                        while(reader.Read()) {
                            int id = reader.GetInt32(0);
                            string descripcion = reader.GetString(1);
                            provincias.Add(new Provincia(id, descripcion));
                        }
                    }
                }

            }
            return provincias;
        }

        private List<Proveedor> ObtenerProveedores () {
            List<Proveedor> proveedores = new List<Proveedor>();
            string cadenaConexion = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion)) {
                conexion.Open();
                string consulta = "SELECT * FROM Proveedor;";
                using (SqlCommand comando = new SqlCommand(consulta, conexion)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
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
            return proveedores;
        }


    }
}
