using ARQ.Recursos;
using Framework.Common;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ARQ.Web.Controllers
{
    public class ReportesController : BaseController
    {
       public ActionResult ReporteDeMateriales()
       {
            return View();
       }

       public JsonResult ObtenerReporteMateriales()
       {
            JsonData jsonData = new JsonData();

            try
            {

            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }

            return Json(jsonData);
        }
    }
}
