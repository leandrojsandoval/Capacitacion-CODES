using ARQ.Entidades;
using ARQ.Recursos;
using ARQ.Servicios.Interfaces;
using ARQ.Web.Models.Venta;
using Framework.Common;
using Framework.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ARQ.Web.Controllers {

    [Route("[controller]/[action]")]
    public class VentaController : BaseController {

        #region Propiedades de servicio

        private IServicioGenerico _servicioGenerico { get; set; }

        public VentaController (IServicioGenerico servicioGenerico) {
            this._servicioGenerico = servicioGenerico;
        }

        #endregion

        #region Paginas

        public IActionResult Listado () {
            return View();
        }

        [HttpGet]
        public IActionResult Detalle () {
            try {
                var ventaVM = new VentaViewModel {
                    activo = true
                };
                return View(ventaVM);
            }
            catch (Exception ex) {
                log.Error(ex);
                return Redirect("/Home/Error");
            }
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detalle (int id) {
            var ventaVM = new VentaViewModel();
            try {
                Venta venta = _servicioGenerico.GetById<Venta>(id);
                this.CargarVMDesdeEntidad(ventaVM, venta);
            }
            catch (Exception ex) {
                log.Error("$Error al buscar la venta con id=" + id + ", Error:", ex);
                return Redirect("/Home/Error");
            }
            return View(ventaVM);
        }

        #endregion

        #region Metodos

        [HttpPost]
        public JsonResult ObtenerVentas (string descripcion, DateTime? fecha, int? cantidad, double? total, int? idCliente, bool? activo) {

            JsonData jsonData = new();
            IList<Venta> ventas = new List<Venta>();

            if (descripcion == null) {
                descripcion = "";
            }

            try {

                ventas = _servicioGenerico.GetAll<Venta>(
                    v => (v.Descripcion.ToLower().Contains(descripcion.ToLower())) &&
                         (!fecha.HasValue || v.Fecha == fecha.Value) &&
                         (!cantidad.HasValue || v.Cantidad == cantidad.Value) &&
                         (!total.HasValue || v.Total == total.Value) &&
                         (!idCliente.HasValue || v.IdCliente == idCliente.Value) &&
                         (!activo.HasValue || v.Activo == activo.Value)).ToList();

                var gridData = ventas.Select(vta => new VentaViewModel() {
                    idVenta = vta.Id,
                    descripcion = vta.Descripcion,
                    fecha = vta.Fecha.ToString("yyyy-MM-dd"),
                    cantidad = vta.Cantidad,
                    total = vta.Total,
                    idCliente = vta.IdCliente,
                    nombreCliente = vta.Cliente.Nombre + " " + vta.Cliente.Apellido,
                    activo = vta.Activo
                }).OrderBy(clientes => clientes.fecha);

                jsonData.content = gridData;
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex) {

                log.Error("No se pudo obtener la lista de ventas con los siguientes parámetros de búsqueda: " +
                    "descripcion: " + descripcion +
                    ", fecha: " + fecha +
                    ", cantidad: " + cantidad +
                    ", total: " + total +
                    ", cliente: " + idCliente +
                    ", activo: " + activo, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;

            }

            return Json(jsonData);

        }




        #endregion

        #region Privados

        private void CargarEntidadDesdeVM (Venta venta, VentaViewModel ventaVM) {
            venta.Descripcion = ventaVM.descripcion;
            venta.Fecha = DateTime.ParseExact(ventaVM.fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //venta.Total = ventaVM.total;
            //venta.Cantidad = ventaVM.cantidad;
            venta.IdCliente = ventaVM.idCliente;
            venta.Activo = ventaVM.activo;
        }

        private void CargarVMDesdeEntidad (VentaViewModel ventaVM, Venta venta) {
            ventaVM.fecha = venta.Fecha.ToString("yyyy-MM-dd");
            ventaVM.descripcion = venta.Descripcion;
            ventaVM.cantidad = venta.Cantidad;
            ventaVM.total = venta.Total;
            ventaVM.idCliente = venta.IdCliente;
            ventaVM.nombreCliente = venta.Cliente.Nombre + " " + venta.Cliente.Apellido;
            ventaVM.activo = venta.Activo;
        }

        #endregion

    }
}
