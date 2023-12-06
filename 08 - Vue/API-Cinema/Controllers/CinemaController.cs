using API_Cinema.Entidades;
using API_Cinema.Servicios.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API_Cinema.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase {

        private ServicioSucursal servicioSucursal;
        private ServicioPelicula servicioPelicula;
        private ServicioHorario servicioHorario;
        private ServicioVenta servicioVenta;

        public CinemaController(IConfiguration configuration) {
            servicioSucursal = new ServicioSucursal(configuration);
            servicioPelicula = new ServicioPelicula(configuration);
            servicioHorario = new ServicioHorario(configuration);
            servicioVenta = new ServicioVenta(configuration);
        }

        /****************************** SUCURSALES ******************************/

        [HttpGet]
        [Route("ObtenerSucursales")]
        public JsonResult ObtenerSucursales() {

            List<Sucursal> sucursales = servicioSucursal.ObtenerSucursales();

            DataTable table = new();

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Precio", typeof(decimal));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));

            foreach (Sucursal sucursal in sucursales) {
                DataRow row = table.NewRow();
                row["Id"] = sucursal.Id;
                row["Nombre"] = sucursal.Nombre;
                row["Precio"] = sucursal.Precio;
                row["FechaCreacion"] = sucursal.FechaCreacion;
                row["FechaModificacion"] = sucursal.FechaActualizacion;

                table.Rows.Add(row);
            }


            return new JsonResult(table);
        }

        [HttpGet]
        [Route("ObtenerSucursalPorId")]
        public JsonResult ObtenerSucursalPorId(int id) {

            List<Sucursal> sucursales = servicioSucursal.ObtenerSucursalPorId(id);

            DataTable table = new();

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Precio", typeof(decimal));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));

            foreach (Sucursal sucursal in sucursales) {
                DataRow row = table.NewRow();
                row["Id"] = sucursal.Id;
                row["Nombre"] = sucursal.Nombre;
                row["Precio"] = sucursal.Precio;
                row["FechaCreacion"] = sucursal.FechaCreacion;
                row["FechaModificacion"] = sucursal.FechaActualizacion;

                table.Rows.Add(row);
            }


            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AgregarSucursal")]
        public JsonResult AgregarSucursal(string nombre, string precio) {

            Sucursal sucursal = new() {
                Nombre = nombre,
                Precio = decimal.Parse(precio)
            };

            servicioSucursal.Insertar(sucursal);

            return new JsonResult("Agregado Correctamente");

        }

        [HttpPost]
        [Route("ActualizarSucursal")]
        public JsonResult ActualizarSucursal(string id, string nombre, string precio) {

            Sucursal sucursal = new() {
                Id = int.Parse(id),
                Nombre = nombre,
                Precio = decimal.Parse(precio)
            };

            servicioSucursal.Actualizar(sucursal);

            return new JsonResult("Actualizado Correctamente");

        }

        /****************************** PELICULAS ******************************/

        [HttpGet]
        [Route("ObtenerPeliculas")]
        public JsonResult ObtenerPeliculas() {

            List<Pelicula> peliculas = servicioPelicula.ObtenerPeliculas();

            DataTable table = new();

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Imagen", typeof(string));
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Descripcion", typeof(string));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));

            foreach (Pelicula pelicula in peliculas) {
                DataRow row = table.NewRow();
                row["Id"] = pelicula.Id;
                row["Imagen"] = pelicula.Imagen;
                row["Nombre"] = pelicula.Nombre;
                row["Descripcion"] = pelicula.Descripcion;
                row["FechaCreacion"] = pelicula.FechaCreacion;
                row["FechaModificacion"] = pelicula.FechaActualizacion;

                table.Rows.Add(row);
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("ObtenerPeliculaPorId")]
        public JsonResult ObtenerPeliculaPorId(int id) {

            Pelicula pelicula = servicioPelicula.ObtenerPeliculaPorId(id);

            DataTable table = new();

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Imagen", typeof(string));
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Descripcion", typeof(string));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));

            DataRow row = table.NewRow();
            row["Id"] = pelicula.Id;
            row["Imagen"] = pelicula.Imagen;
            row["Nombre"] = pelicula.Nombre;
            row["Descripcion"] = pelicula.Descripcion;
            row["FechaCreacion"] = pelicula.FechaCreacion;
            row["FechaModificacion"] = pelicula.FechaActualizacion;

            table.Rows.Add(row);

            return new JsonResult(table);
        }

        /****************************** HORARIOS ******************************/

        [HttpPost]
        [Route("AgregarHorario")]
        public JsonResult AgregarHorario(string idSucursal, string idPelicula, string hora) {

            Horario horario = new() {
                IdSucursal = int.Parse(idSucursal),
                IdPelicula = int.Parse(idPelicula),
                Hora = hora
            };

            servicioHorario.Insertar(horario);

            return new JsonResult("Agregado Correctamente");

        }

        /****************************** VENTAS ******************************/

        [HttpGet]
        [Route("ObtenerVentas")]
        public JsonResult ObtenerVentas() {

            List<Venta> ventas = servicioVenta.ObtenerVentas();

            DataTable table = new();

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("IdUsuario", typeof(int));
            table.Columns.Add("IdHorario", typeof(int));
            table.Columns.Add("Cantidad", typeof(int));
            table.Columns.Add("Total", typeof(decimal));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));

            foreach (Venta venta in ventas) {
                DataRow row = table.NewRow();
                row["Id"] = venta.Id;
                row["IdUsuario"] = venta.IdUsuario;
                row["IdHorario"] = venta.IdHorario;
                row["Cantidad"] = venta.Cantidad;
                row["Cantidad"] = venta.Cantidad;
                row["Total"] = venta.Total;
                row["FechaCreacion"] = venta.FechaCreacion;
                row["FechaModificacion"] = venta.FechaActualizacion;

                table.Rows.Add(row);
            }


            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AgregarVenta")]
        public JsonResult AgregarVenta(string idUsuario, string idHorario, string cantidad, string total) {

            Venta venta = new() {
                IdUsuario = int.Parse(idUsuario),
                IdHorario = int.Parse(idHorario),
                Cantidad = int.Parse(cantidad),
                Total = decimal.Parse(total)
            };

            servicioVenta.Insertar(venta);

            return new JsonResult("Agregado Correctamente");

        }

        /****************************** USUARIO ******************************/

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login([FromBody] LoginModel model) {
            if (ModelState.IsValid) {
                // Lógica de autenticación aquí, por ejemplo, verificar credenciales en una base de datos.
                bool isAuthenticated = YourAuthenticationService.Authenticate(model.Username, model.Password);

                if (isAuthenticated) {
                    // El usuario está autenticado, puedes devolver un token JWT u otra información según tus necesidades.
                    string token = YourAuthenticationService.GenerateToken(model.Username);
                    return Ok(new { Token = token });
                }
                else {
                    // El usuario no está autenticado, devuelve un mensaje de error.
                    return BadRequest("Credenciales inválidas");
                }
            }

            // El modelo no es válido, devuelve un error de modelo.
            return BadRequest(ModelState);
        }
    }
}

}
