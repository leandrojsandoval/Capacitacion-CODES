using PracticaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PracticaMVC.Controllers {
    public class ProvinciaController : Controller {

        public ActionResult Index() {
            IList<ProvinciaModel> provincias = new List<ProvinciaModel> {
                new ProvinciaModel() { Id = 1, Descripcion = "Buenos Aires" },
                new ProvinciaModel() { Id = 2, Descripcion = "Córdoba" },
                new ProvinciaModel() { Id = 3, Descripcion = "Entre Ríos" }
            };
            return View(provincias);
        }

        private IList<ProvinciaModel> ObtenerProvincias() {
            IList<ProvinciaModel> provincias = new List<ProvinciaModel> {
                new ProvinciaModel() { Id = 1, Descripcion = "Buenos Aires" },
                new ProvinciaModel() { Id = 2, Descripcion = "Córdoba" },
                new ProvinciaModel() { Id = 3, Descripcion = "Entre Ríos" }
            };
            return provincias;
        }

        public ActionResult Edit(int idProvincia) {
            IList<ProvinciaModel> provincias = ObtenerProvincias();
            // OJO! Esto se resolvería con la consulta al correspondiente negocio.
            // Es para poder ver en el ejemplo el ActionResult
            ProvinciaModel provincia =
                (from prov in provincias
                 where prov.Id == idProvincia
                 select prov).First();
            return View("Edit", provincia);
        }

        [HttpPost]
        public ActionResult Edit(ProvinciaModel provincia) {
            if (ModelState.IsValid) {
                //... Aquí va el código para almacenar los cambios
                return RedirectToAction("Index");
            }
            return View("Edit", provincia);
        }
        public string CargarPopup() {
            DatosPopup datos = new DatosPopup {
                InSitu = true,
                Tercerizado = false,
                TiposTrabajo = new List<TipoTrabajo> {
                new TipoTrabajo() { Id = 1, Descripcion = "Tipo1" },
                new TipoTrabajo() { Id = 2, Descripcion = "Tipo2" },
                new TipoTrabajo() { Id = 3, Descripcion = "Tipo3" }
                }
            };
            System.IO.StringWriter sw = new System.IO.StringWriter();
            ViewEngineResult ver = ViewEngines.Engines.FindPartialView(this.ControllerContext, "DatosPopup");
            this.ViewData.Model = datos;
            ViewContext vc = new ViewContext(this.ControllerContext, ver.View, this.ViewData, this.TempData, sw);
            ver.View.Render(vc, sw);
            return sw.GetStringBuilder().ToString();
        }

        public string GuardarDatosPopup(DatosPopup datosPopup) {
            string respuesta = "";
            try {
                if (ModelState.IsValid) {
                    respuesta = "TODO OK!!!";   // TODO: Realizar las operaciones necesarias de la ViewModel
                }
            }
            catch (Exception ex) {
                respuesta = ex.Message;
            }
            return respuesta;
        }
    }
}