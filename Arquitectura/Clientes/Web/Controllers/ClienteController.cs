using ARQ.Entidades;
using ARQ.Recursos;
using ARQ.Servicios.Interfaces;
using ARQ.Servicios.RelationshipValidators;
using ARQ.Web.Models.Cliente;
using Framework.Common;
using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ARQ.Web.Controllers {

    [Route("[controller]/[action]")]
    public class ClienteController : BaseController {

        #region Propiedades de servicio

        private IServicioGenerico _servicioGenerico { get; set; }
        private IServicioClientes _servicioClientes { get; set; }

        public ClienteController (IServicioGenerico servicioGenerico, IServicioClientes servicioClientes) {
            this._servicioGenerico = servicioGenerico;
            this._servicioClientes = servicioClientes;
        }

        #endregion

        #region Paginas

        public IActionResult Listado () {
            return View();
        }

        [HttpGet]
        public IActionResult Detalle () {
            try {
                var clienteVM = new ClienteViewModel {
                    activo = true
                };
                return View(clienteVM);
            }
            catch (Exception ex) {
                log.Error(ex);
                return Redirect("/Home/Error");
            }
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detalle (int id) {
            var clienteVM = new ClienteViewModel();
            try {
                Cliente cliente = _servicioGenerico.GetById<Cliente>(id);
                this.CargarVMdesdeEntidad(clienteVM, cliente);
            }
            catch (Exception ex) {
                log.Error("$Error al buscar el cliente con id=" + id + ", Error:", ex);
                return Redirect("/Home/Error");
            }
            return View(clienteVM);
        }

        #endregion

        #region Metodos

        [HttpPost]
        public JsonResult ObtenerClientes (string nombre, string apellido, DateTime? fechaNacimiento, bool? activo) {

            JsonData jsonData = new();
            IList<Cliente> clientes = new List<Cliente>();

            if (nombre == null) {
                nombre = "";
            }

            if (apellido == null) {
                apellido = "";
            }

            try {

                clientes = _servicioGenerico.GetAll<Cliente>(c => (c.Nombre.ToLower().Contains(nombre.ToLower())) &&
                                                                   (c.Apellido.ToLower().Contains(apellido.ToLower())) &&
                                                                    (!fechaNacimiento.HasValue || c.FechaNacimiento == fechaNacimiento.Value) &&
                                                                    (!activo.HasValue || c.Activo == activo.Value)).ToList();

                //clientes = _servicioClientes.ObtenerClientes(nombre, apellido, fechaNacimiento, activo);

                var gridData = clientes.Select(cli => new ClienteViewModel() {
                    idCliente = cli.Id,
                    nombre = cli.Nombre,
                    apellido = cli.Apellido,
                    fechaNacimiento = cli.FechaNacimiento.ToString("yyyy-MM-dd"),
                    activo = cli.Activo,
                }).OrderBy(clientes => clientes.nombre);

                jsonData.content = gridData;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex) {
                log.Error("No se pudo obtener la lista de clientes con los siguientes parámetros de búsqueda: nombre: " + nombre + ", apellido: " + apellido + ", fecha nacimiento: " + fechaNacimiento + ", activo: " + activo, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }

            return Json(jsonData);

        }

        public Task<JsonData> Inactivar (int clienteId) {

            JsonData jsonData = new();

            try {
                Cliente cliente = _servicioGenerico.GetById<Cliente>(clienteId);
                _servicioGenerico.Deactivate<Cliente>(cliente);
                jsonData.result = JsonData.Result.Ok;
            }
            catch (RelatedEntityException rex) {
                log.Error(rex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = rex.Message;
                jsonData.result = JsonData.Result.Error;
            }
            catch (Exception ex) {
                log.Error("$Error al inactivar el cliente con id=" + clienteId + ", Error:", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }

            return Task.FromResult(jsonData);

        }

        [HttpPost]
        public Task<JsonData> Guardar (ClienteViewModel clienteVM) {

            IList<string> mensajes = new List<string>();
            JsonData jsonData = new();

            try {

                if (!ModelState.IsValid) {
                    this.CargarErroresModelo(mensajes, jsonData);
                    Response.StatusCode = Constantes.ERROR_HTTP;
                    return Task.FromResult(jsonData);
                }

                var esAlta = !clienteVM.idCliente.HasValue;
                var appval = this._servicioGenerico.Get<Cliente>(mat => mat.Nombre == clienteVM.nombre);

                if (esAlta && appval != null) {

                    jsonData.result = JsonData.Result.ModelValidation;
                    jsonData.errorUi = Global.PorFavorRevise;
                    jsonData.errors = new { Nombre = $"El nombre '{clienteVM.nombre}' ya existe." };

                    Response.StatusCode = Constantes.ERROR_HTTP;
                    return Task.FromResult(jsonData);

                }

                Cliente cliente = esAlta ? new Cliente() : this._servicioGenerico.GetById<Cliente>(clienteVM.idCliente.Value);
                this.CargarEntidadDesdeVM(cliente, clienteVM);

                if (esAlta) {
                    try {
                        cliente.IdUsuarioAlta = UserUtils.GetId(User);
                        this._servicioGenerico.Add(cliente);
                    }
                    catch (Exception ex) {
                        log.Error("No se pudo dar de alta el nuevo material. Error: ", ex);
                        Response.StatusCode = Constantes.ERROR_HTTP;
                        jsonData.errorUi = "No se pudo dar de alta el nuevo material";
                        jsonData.result = JsonData.Result.Error;
                        return Task.FromResult(jsonData);
                    }
                }
                else {
                    try {
                        cliente.IdUsuarioModificacion = UserUtils.GetId(User);
                        this._servicioGenerico.Update(cliente);
                    }
                    catch (RelatedEntityException rex) {
                        log.Error(rex);
                        Response.StatusCode = Constantes.ERROR_HTTP;
                        jsonData.errorUi = rex.Message;
                        jsonData.result = JsonData.Result.Error;
                        return Task.FromResult(jsonData);
                    }
                    catch (Exception ex) {
                        log.Error("No se pudieron actualizar los datos del cliente con id: " + clienteVM.idCliente + " Error: ", ex);
                        Response.StatusCode = Constantes.ERROR_HTTP;
                        jsonData.errorUi = "No se pudieron actualizar los datos del cliente: " + clienteVM.nombre;
                        jsonData.result = JsonData.Result.Error;
                        return Task.FromResult(jsonData);
                    }
                }

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex) {
                log.Error(ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }

            return Task.FromResult(jsonData);

        }

        [HttpGet]
        public IActionResult Exportar (string nombre, string apellido, DateTime? fechaNacimiento, bool? activo) {

            List<ClienteViewModel> listaClientes = new();

            if (nombre == null) {
                nombre = "";
            }

            if (apellido == null) {
                apellido = "";
            }

            try {
                listaClientes = _servicioGenerico.GetAll<Cliente>(c =>
                    (c.Nombre.ToLower().Contains(nombre.ToLower())) &&
                    (c.Apellido.ToLower().Contains(apellido.ToLower())) &&
                    (!fechaNacimiento.HasValue || c.FechaNacimiento == fechaNacimiento.Value) &&
                    (!activo.HasValue || c.Activo == activo.Value)
                    ).Select(cliente => new ClienteViewModel() {
                        idCliente = cliente.Id,
                        nombre = cliente.Nombre,
                        apellido = cliente.Apellido,
                        fechaNacimiento = cliente.FechaNacimiento.ToString("yyyy-MM-dd"),
                        activo = cliente.Activo,
                    }).ToList();

                log.Info("Método GetAll OK");

                var listaReducida = listaClientes.Select(cli => new {
                    cli.nombre,
                    cli.apellido,
                    fechaNacimiento = cli.fechaNacimiento,
                    activo = cli.activo == true ? Global.Activo : Global.Inactivo,
                }).ToList();

                var fileBytes = CreateExcelFile.CreateExcelDocumentAsByte(listaReducida);
                log.Info("Método crear excel OK");
                this.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                this.HttpContext.Response.Headers.Add("content-disposition", "attachment");
                return File(fileBytes, this.HttpContext.Response.ContentType);
            }
            catch (Exception ex) {
                this.HttpContext.Response.StatusCode = Constantes.ERROR_HTTP;
                log.Error(ex);
            }
            return Content(string.Empty);
        }

        #endregion

        #region Privados

        private void CargarEntidadDesdeVM (Cliente cliente, ClienteViewModel clienteVM) {
            cliente.Nombre = clienteVM.nombre;
            cliente.Apellido = clienteVM.apellido;
            cliente.FechaNacimiento = DateTime.ParseExact(clienteVM.fechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            cliente.Activo = clienteVM.activo;
        }

        private void CargarVMdesdeEntidad (ClienteViewModel clienteVM, Cliente cliente) {
            clienteVM.idCliente = cliente.Id;
            clienteVM.nombre = cliente.Nombre;
            clienteVM.apellido = cliente.Apellido;
            clienteVM.fechaNacimiento = cliente.FechaNacimiento.ToString("yyyy-MM-dd");
            clienteVM.activo = cliente.Activo;
        }

        #endregion

    }
}
