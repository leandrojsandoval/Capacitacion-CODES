using Web.Models;

namespace ARQ.Web.Models.Venta {
    public class VentaViewModel : BaseViewModel {

        public int? idVenta { get; set; }
        public string descripcion { get; set; }
        public string fecha { get; set; }
        public double total { get; set; }
        public int cantidad { get; set; }
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public bool activo { get; set; }

    }

}
