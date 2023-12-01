using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Cinema.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase {

        public const string SP_OBTENER_SUCURSALES = "sp_obtener_sucursales";
        public const string SP_OBTENER_SUCURSAL_POR_ID = "sp_obtener_sucursal_por_id";

        private IConfiguration _configuration;

        public CinemaController(IConfiguration configuration) {
            _configuration = configuration;
        }

        /****************************** SUCURSALES ******************************/

        [HttpGet]
        [Route("GetSucursales")]
        public JsonResult GetSucursales() {
            DataTable table = new();
            string sqlDataSource = _configuration.GetConnectionString("CinemaDB");
            SqlDataReader reader;
            using (SqlConnection connection = new(sqlDataSource)) {
                connection.Open();
                using (SqlCommand command = new(SP_OBTENER_SUCURSALES, connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetSucursalPorId")]
        public JsonResult GetSucursalPorId(int id) {
            DataTable table = new();
            string sqlDataSource = _configuration.GetConnectionString("CinemaDB");
            SqlDataReader reader;
            using (SqlConnection connection = new(sqlDataSource)) {
                connection.Open();
                using (SqlCommand command = new(SP_OBTENER_SUCURSAL_POR_ID, connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(table);
        }

        /*[HttpPost]
        [Route("AddSucursal")]
        public JsonResult AddSucursal(Surcursal sucursal) {
            DataTable table = new();
            string sqlDataSource = _configuration.GetConnectionString("CinemaDB");
            SqlDataReader reader;
            using (SqlConnection connection = new(sqlDataSource)) {
                connection.Open();
                using (SqlCommand command = new(SP_OBTENER_SUCURSAL_POR_ID, connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(table);
        }*/

    }

}
