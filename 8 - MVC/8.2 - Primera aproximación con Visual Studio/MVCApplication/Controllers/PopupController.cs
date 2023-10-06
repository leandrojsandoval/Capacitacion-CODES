using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MVCApplication.Controllers {
    public class PopupController : Controller {
        // GET: Popup
        public ActionResult Index() {
            return View();
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
            StringWriter sw = new StringWriter();
            ViewEngineResult ver = ViewEngines.Engines.FindPartialView(this.ControllerContext, "DatosPopup");
            this.ViewData.Model = datos;
            ViewContext vc = new ViewContext(this.ControllerContext, ver.View, this.ViewData, this.TempData, sw);
            ver.View.Render(vc, sw);
            return sw.GetStringBuilder().ToString();
        }

        public string GuardarDatosPopup(DatosPopup datosPopup) {
            string respuesta = " ";
            try {
                if (ModelState.IsValid) {
                    respuesta = "TODO OK!!!"; // TODO: Realizar las operaciones necesarias de la ViewModel
                }
            }
            catch (Exception ex) {
                respuesta = ex.Message;
            }
            return respuesta;
        }
    }
}