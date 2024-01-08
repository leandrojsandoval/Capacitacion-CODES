using ARQ.Entidades;
using ARQ.Recursos;
using ARQ.Servicios.Interfaces;
using ARQ.Servicios.RelationshipValidators;
using ARQ.Web.Models.Material;
using Framework.Common;
using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARQ.Web.Controllers {

    [Route("[controller]/[action]")]
    public class MaterialController : BaseController {

        #region Propiedades de servicio

        private IServicioGenerico _servicioGenerico { get; set; }

        public MaterialController (IServicioGenerico servicioGenerico) {
            this._servicioGenerico = servicioGenerico;
        }

        /* 
         * private IServicioMateriales _servicioMateriales { get; set; }
         * public MaterialController(IServicioGenerico servicioGenerico, IServicioMateriales servicioMateriales) {
         *      this._servicioGenerico = servicioGenerico;
         *      this._servicioMateriales = servicioMateriales;
         * } 
         */

        #endregion

        #region Paginas        

        [HttpGet]
        public IActionResult Listado () {
            var venta = _servicioGenerico.Count<Venta>(v => v.Monto > 200);
            return View();
        }

        [HttpGet]
        public IActionResult Detalle () {

            try {
                var materialVM = new MaterialViewModel {
                    activo = true
                };
                return View(materialVM);
            }
            catch (Exception ex) {
                log.Error(ex);
                return Redirect("/Home/Error");
            }

        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detalle (int id) {
            
            var materialVM = new MaterialViewModel();

            try {
                Material material = _servicioGenerico.GetById<Material>(id);
                this.CargarVMdesdeEntidad(materialVM, material);
            }
            catch (Exception ex) {
                log.Error("$Error al buscar el material con id=" + id + ", Error:", ex);
                return Redirect("/Home/Error");
            }

            return View(materialVM);

        }
        
        #endregion

        #region Metodos

        [HttpPost]
        public JsonResult ObtenerMateriales (string nombre, string descripcion, double? multiplicador, bool? activo) {
            
            JsonData jsonData = new();
            IList<Material> materiales = new List<Material>();

            if (nombre == null) {
                nombre = "";
            }

            if (descripcion == null) {
                descripcion = "";
            }

            try {
                materiales = _servicioGenerico.GetAll<Material>(m => (m.Nombre.ToLower().Contains(nombre.ToLower())) &&
                                                                    (m.Descripcion.ToLower().Contains(descripcion.ToLower())) &&
                                                                    (!multiplicador.HasValue || m.MultiplicadorToneladas == multiplicador.Value) &&
                                                                    (!activo.HasValue || m.Activo == activo.Value)).ToList();

                //materiales = _servicioMateriales.ObtenerMateriales(nombre,descripcion,multiplicador,activo);

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

            catch (Exception ex) {
                log.Error("No se pudo obtener la lista de materiales con los siguientes parámetros de búsqueda: nombre: " + nombre + ", descripcion: " + descripcion + ", multiplicador: " + multiplicador + ", activo: " + activo, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }

            return Json(jsonData);

        }

        public Task<JsonData> Inactivar (int materialId) {

            JsonData jsonData = new();

            try {
                Material material = _servicioGenerico.GetById<Material>(materialId);
                _servicioGenerico.Deactivate<Material>(material);
                jsonData.result = JsonData.Result.Ok;
            }
            catch (RelatedEntityException rex) {
                log.Error(rex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = rex.Message;
                jsonData.result = JsonData.Result.Error;
            }
            catch (Exception ex) {
                log.Error("$Error al inactivar el material con id=" + materialId + ", Error:", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }

            return Task.FromResult(jsonData);

        }

        [HttpPost]
        public Task<JsonData> Guardar (MaterialViewModel materialVM) {

            IList<string> mensajes = new List<string>();
            JsonData jsonData = new();

            try {
                
                if (!ModelState.IsValid) {
                    this.CargarErroresModelo(mensajes, jsonData);
                    Response.StatusCode = Constantes.ERROR_HTTP;
                    return Task.FromResult(jsonData);
                }

                var esAlta = !materialVM.idMaterial.HasValue;
                var appval = this._servicioGenerico.Get<Material>(mat => mat.Nombre == materialVM.nombre);
                
                if (esAlta && appval != null) {

                    jsonData.result = JsonData.Result.ModelValidation;
                    jsonData.errorUi = Global.PorFavorRevise;
                    jsonData.errors = new { Nombre = $"El nombre '{materialVM.nombre}' ya existe." };

                    Response.StatusCode = Constantes.ERROR_HTTP;
                    return Task.FromResult(jsonData);

                }

                Material material = esAlta ? new Material() : this._servicioGenerico.GetById<Material>(materialVM.idMaterial.Value);
                this.CargarEntidadDesdeVM(material, materialVM);
                
                if (esAlta) {
                    try {
                        material.IdUsuarioAlta = UserUtils.GetId(User);
                        this._servicioGenerico.Add(material);
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
                        material.IdUsuarioModificacion = UserUtils.GetId(User);
                        this._servicioGenerico.Update(material);
                    }
                    catch (RelatedEntityException rex) {
                        log.Error(rex);
                        Response.StatusCode = Constantes.ERROR_HTTP;
                        jsonData.errorUi = rex.Message;
                        jsonData.result = JsonData.Result.Error;
                        return Task.FromResult(jsonData);
                    }
                    catch (Exception ex) {
                        log.Error("No se pudieron actualizar los datos del material con id: " + materialVM.idMaterial + " Error: ", ex);
                        Response.StatusCode = Constantes.ERROR_HTTP;
                        jsonData.errorUi = "No se pudieron actualizar los datos del material: " + materialVM.nombre;
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
        public IActionResult Exportar (string nombre, string descripcion, double? multiplicador, bool? activo) {
            
            List<MaterialViewModel> listaMateriales = new();

            if (nombre == null) {
                nombre = "";
            }

            if (descripcion == null) {
                descripcion = "";
            }

            try {
                listaMateriales = _servicioGenerico.GetAll<Material>(m =>
                    (m.Nombre.ToLower().Contains(nombre.ToLower())) && 
                    (m.Descripcion.ToLower().Contains(descripcion.ToLower())) && 
                    (!multiplicador.HasValue || m.MultiplicadorToneladas == multiplicador.Value) && 
                    (!activo.HasValue || m.Activo == activo.Value)
                    ).Select(material => new MaterialViewModel() {
                        idMaterial = material.Id,
                        nombre = material.Nombre,
                        descripcion = material.Descripcion,
                        multiplicadorToneladas = material.MultiplicadorToneladas,
                        activo = material.Activo,
                }).ToList();

                log.Info("Método GetAll OK");

                var listaReducida = listaMateriales.Select(mat => new {
                    mat.nombre,
                    mat.descripcion,
                    multiplicador = mat.multiplicadorToneladas,
                    activo = mat.activo == true ? Global.Activo : Global.Inactivo,
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
        
        private void CargarEntidadDesdeVM (Material material, MaterialViewModel materialVM) {
            material.Nombre = materialVM.nombre;
            material.Descripcion = materialVM.descripcion;
            material.MultiplicadorToneladas = materialVM.multiplicadorToneladas;
            material.Activo = materialVM.activo;
        }

        private void CargarVMdesdeEntidad (MaterialViewModel materialVM, Material material) {
            materialVM.idMaterial = material.Id;
            materialVM.nombre = material.Nombre;
            materialVM.descripcion = material.Descripcion;
            materialVM.multiplicadorToneladas = material.MultiplicadorToneladas;
            materialVM.activo = material.Activo;
        }

        #endregion
    
    }

}
