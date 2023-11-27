using System.Collections.Generic;

namespace PracticaMVC.Models {
    public class DatosPopup {
        public bool InSitu { get; set; }
        public bool Tercerizado { get; set; }
        public IList<TipoTrabajo> TiposTrabajo { get; set; }
        public int IdTipoTrabajo { get; set; }
    }
    public class TipoTrabajo {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}