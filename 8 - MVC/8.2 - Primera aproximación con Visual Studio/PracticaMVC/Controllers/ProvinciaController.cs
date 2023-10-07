using PracticaMVC.Models;
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

        // METODO MIO
        [NonAction]
        public IList<ProvinciaModel> ObtenerProvincias() {
            IList<ProvinciaModel> provincias = new List<ProvinciaModel>
            {
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

        public ActionResult Edit(ProvinciaModel provincia) {
            if (ModelState.IsValid) {
                //... Aquí va el código para almacenar los cambios
                return RedirectToAction("Index");
            }
            return View("Edit", provincia);
        }
    }
}