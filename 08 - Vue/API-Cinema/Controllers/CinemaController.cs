using API_Cinema.Entidades;
using API_Cinema.Framework.Common;
using API_Cinema.Servicios.Implementaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Cinema.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase {

        private ServicioSucursal servicioSucursal;
        private ServicioPelicula servicioPelicula;
        public CinemaController(IConfiguration configuration) {
            servicioSucursal = new ServicioSucursal(configuration);
            servicioPelicula = new ServicioPelicula(configuration);
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


    }

}
