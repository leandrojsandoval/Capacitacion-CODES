using ARQ.Entidades;
using ARQ.Recursos;
using ARQ.Servicios.Interfaces;
using ARQ.Web.Models.Material;
using Framework.Common;
using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARQ.Web.Controllers
{

    [Route("[controller]/[action]")]
    public class MaterialController : BaseController
    {
        private const string EXTENSION_MIME_XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        #region Propiedades de servicio

        private IServicioGenerico _servicioGenerico { get; set; }

        public MaterialController (IServicioGenerico servicioGenerico)
        {
            _servicioGenerico = servicioGenerico;
        }

        #endregion

        #region Paginas        

        [HttpGet]
        public IActionResult Listado ()
        {
            var venta = _servicioGenerico.Count<Venta>(v => v.Monto > 200);
            return View();
        }

        [HttpGet]
        public IActionResult Detalle ()
        {

            try
            {
                var materialVM = new MaterialViewModel {
                    activo = true
                };
                return View(materialVM);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Redirect("/Home/Error");
            }

        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detalle (int id)
        {
            var materialVM = new MaterialViewModel();

            try
            {
                Material material = _servicioGenerico.GetById<Material>(id);
                this.cargarVMdesdeEntidad(materialVM, material);
            }
            catch (Exception ex)
            {
                log.Error(String.Format(Global.MensajeMaterialErrorDetalleId, id) + ", Error:", ex);
                return Redirect("/Home/Error");
            }

            return View(materialVM);
        }

        #endregion

        #region Metodos

        [HttpPost]
        public JsonResult ObtenerMateriales (string nombre, string descripcion, double? multiplicador, bool? activo)
        {
            JsonData jsonData = new();
            IList<Material> materiales = new List<Material>();

            nombre ??= "";
            descripcion ??= "";

            try
            {
                materiales = _servicioGenerico.GetAll<Material>(m =>
                    m.Nombre.ToLower().Contains(nombre.ToLower()) &&
                    m.Descripcion.ToLower().Contains(descripcion.ToLower()) &&
                    (!multiplicador.HasValue || m.MultiplicadorToneladas == multiplicador.Value) &&
                    (!activo.HasValue || m.Activo == activo.Value)).
                ToList();

                var gridData = materiales.Select(mat => new MaterialViewModel() {
                    idMaterial = mat.Id,
                    nombre = mat.Nombre,
                    descripcion = mat.Descripcion,
                    multiplicadorToneladas = mat.MultiplicadorToneladas,
                    activo = mat.Activo,
                }).OrderBy(materiales => materiales.nombre);

                jsonData.content = gridData;
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(String.Format(Global.MensajeMaterialErrorLista, nombre, descripcion, multiplicador, activo), ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = Global.ErrorGenerico;
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public Task<JsonData> Inactivar (int materialId)
        {
            JsonData jsonData = new();

            try
            {
                Material material = _servicioGenerico.GetById<Material>(materialId);
                _servicioGenerico.Deactivate<Material>(material);
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(String.Format(Global.MensajeMaterialInactivar, materialId) + ", Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = Global.ErrorGenerico;
                jsonData.result = JsonData.Result.Error;
            }

            return Task.FromResult(jsonData);
        }

        [HttpPost]
        public Task<JsonData> Guardar (MaterialViewModel materialVM)
        {
            IList<string> mensajes = new List<string>();
            JsonData jsonData = new();

            try
            {

                if (!ModelState.IsValid)
                {
                    CargarErroresModelo(mensajes, jsonData);
                    Response.StatusCode = Constantes.ERROR_HTTP;
                    return Task.FromResult(jsonData);
                }

                var esAlta = !materialVM.idMaterial.HasValue;
                var appval = _servicioGenerico.Get<Material>(material => material.Nombre == materialVM.nombre);

                if (esAlta && appval != null)
                {
                    if(Response != null)
                        Response.StatusCode = Constantes.ERROR_HTTP;
                    jsonData.errors = new { Nombre = String.Format(Global.MensajeMaterialExistente, materialVM.nombre) };
                    jsonData.errorUi = Global.PorFavorRevise;
                    jsonData.result = JsonData.Result.ModelValidation;
                    return Task.FromResult(jsonData);
                }

                Material material = esAlta ? new Material() : _servicioGenerico.GetById<Material>(materialVM.idMaterial.Value);
                cargarEntidadDesdeVM(material, materialVM);

                if (esAlta)
                {
                    try
                    {
                        material.IdUsuarioAlta = UserUtils.GetId(User);
                        material.IdProducto = 1;
                        _servicioGenerico.Add(material);
                    }
                    catch (Exception ex)
                    {
                        log.Error(Global.MensajeMaterialErrorAlta + " Error: ", ex);
                        if (Response != null)
                            Response.StatusCode = Constantes.ERROR_HTTP;
                        jsonData.errorUi = Global.MensajeMaterialErrorAlta;
                        jsonData.result = JsonData.Result.Error;
                        return Task.FromResult(jsonData);
                    }
                }
                else
                {
                    try
                    {
                        material.IdUsuarioModificacion = UserUtils.GetId(User);
                        _servicioGenerico.Update(material);
                    }
                    catch (Exception ex)
                    {
                        log.Error(String.Format(Global.MensajeMaterialErrorActualizarId, materialVM.idMaterial) + " Error: ", ex);
                        if (Response != null)
                            Response.StatusCode = Constantes.ERROR_HTTP;
                        jsonData.errorUi = String.Format(Global.MensajeMaterialErrorActualizarNombre, materialVM.nombre);
                        jsonData.result = JsonData.Result.Error;
                        return Task.FromResult(jsonData);
                    }
                }
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                if (Response != null)
                    Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }

            return Task.FromResult(jsonData);
        }

        [HttpGet]
        public IActionResult Exportar (string nombre, string descripcion, double? multiplicador, bool? activo)
        {
            List<MaterialViewModel> listaMateriales = new();

            nombre ??= "";
            descripcion ??= "";

            try
            {
                listaMateriales = _servicioGenerico.GetAll<Material>(m =>
                    m.Nombre.ToLower().Contains(nombre.ToLower()) &&
                    m.Descripcion.ToLower().Contains(descripcion.ToLower()) &&
                    (!multiplicador.HasValue || m.MultiplicadorToneladas == multiplicador.Value) &&
                    (!activo.HasValue || m.Activo == activo.Value)
                    ).Select(material => new MaterialViewModel() {
                        idMaterial = material.Id,
                        nombre = material.Nombre,
                        descripcion = material.Descripcion,
                        multiplicadorToneladas = material.MultiplicadorToneladas,
                        activo = material.Activo,
                    }).ToList();

                log.Info(Global.MetodoGetAllOk);

                var listaReducida = listaMateriales.Select(material => new {
                    material.nombre,
                    material.descripcion,
                    multiplicador = material.multiplicadorToneladas,
                    activo = material.activo == true ? Global.Activo : Global.Inactivo,
                }).ToList();

                var fileBytes = CreateExcelFile.CreateExcelDocumentAsByte(listaReducida);

                log.Info(Global.MetodoCrearExcelOk);

                HttpContext.Response.ContentType = EXTENSION_MIME_XLSX;
                HttpContext.Response.Headers.Add("content-disposition", "attachment");

                return File(fileBytes, HttpContext.Response.ContentType);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = Constantes.ERROR_HTTP;
                log.Error(ex);
            }
            
            return Content(string.Empty);
        }

        #endregion

        #region Privados

        private void cargarEntidadDesdeVM (Material material, MaterialViewModel materialVM)
        {
            material.Nombre = materialVM.nombre;
            material.Descripcion = materialVM.descripcion;
            material.MultiplicadorToneladas = materialVM.multiplicadorToneladas;
            material.Activo = materialVM.activo;
        }

        private void cargarVMdesdeEntidad (MaterialViewModel materialVM, Material material)
        {
            materialVM.idMaterial = material.Id;
            materialVM.nombre = material.Nombre;
            materialVM.descripcion = material.Descripcion;
            materialVM.multiplicadorToneladas = material.MultiplicadorToneladas;
            materialVM.activo = material.Activo;
        }

        #endregion

    }

}
