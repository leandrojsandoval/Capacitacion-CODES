using System;
using Web.Models;

namespace ARQ.Web.Models.Cliente {
    public class ClienteViewModel : BaseViewModel {

        public int? idCliente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string fechaNacimiento { get; set; }
        public bool activo { get; set; }

    }

}
