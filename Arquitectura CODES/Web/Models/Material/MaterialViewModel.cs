using Web.Models;

namespace ARQ.Web.Models.Material {

    public class MaterialViewModel : BaseViewModel {

        public int? idMaterial { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double multiplicadorToneladas { get; set; }
        public bool activo { get; set; }

    }

}

