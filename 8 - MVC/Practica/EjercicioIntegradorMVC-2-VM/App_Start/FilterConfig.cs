using System.Web;
using System.Web.Mvc;

namespace EjercicioIntegradorMVC_2_VM {
    public class FilterConfig {
        public static void RegisterGlobalFilters (GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
