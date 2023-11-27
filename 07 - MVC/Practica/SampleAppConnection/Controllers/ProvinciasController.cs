using SampleAppConnection.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace SampleAppConnection.Controllers {
    public class ProvinciasController : Controller {
        
        public ActionResult Index() {
            return View();
        }

        public ActionResult Provincias() {
            IList<Provincia> lista = GetProvinces();
            return View(lista);
        }

        /* NonAction: Indica que no se va a poder realizar una petición desde el 
         * navegador, si no que se va a manejar directamente desde el controlador.*/
        [NonAction] 
        public IList<Provincia> GetProvinces() {
            IList<Provincia> list = new List<Provincia>();
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString").ToString();
            using (SqlConnection conexión = new SqlConnection(connectionString)) {
                if (conexión.State != System.Data.ConnectionState.Open)
                    conexión.Open();
                SqlCommand comando = new SqlCommand("SELECT * FROM Provincia;", conexión);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        list.Add(new Provincia() {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                        });
                    }
                }
                reader.Close();
            }
            return list;
        }
    
    }
}