using MVCApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCApplication.Controllers {
    public class ProvinciaController : Controller {
        // GET: Provincia
        public ActionResult Index() {

            IList<ProvinciaModel> provincias = new List<ProvinciaModel> {
                new ProvinciaModel() { Id = 1, Descripcion = "Buenos Aires" },
                new ProvinciaModel() { Id = 2, Descripcion = "Córdoba" },
                new ProvinciaModel() { Id = 3, Descripcion = "Entre Rios" }
            };

            return View(provincias);
        }

        public ActionResult Edit(int idProvincia) {
            //IList<ProvinciaModel> provincias = ObtenerProvincias();
            IList<ProvinciaModel> provincias = null;

            // OJO! Esto se resolvera con la consulta al correspondiente
            // negocio. Es para poder ver en el ejemplo el ActionResult

            ProvinciaModel provincia =
                (from prov in provincias
                 where prov.Id == idProvincia
                 select prov).First();
            return View("Edit", provincia);
        }

        public ActionResult Edit(ProvinciaModel provincia) {
            if (ModelState.IsValid) {
                // ... Aqui va el codigo
                // ... para almacenar los cambios

                return RedirectToAction("Idex");
            }
            return View("Edit", provincia);
        }
    }
}