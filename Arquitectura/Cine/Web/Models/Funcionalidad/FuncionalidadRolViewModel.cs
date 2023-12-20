using Web.Models;

namespace ARQ.Web.Models.Funcionalidad
{
    public class FuncionalidadRolViewModel : BaseViewModel
    {
        public int idFuncionalidad { get; set; }

        public string nombreFuncionalidad { get; set; }

        public int idRol { get; set; }

        public string nombreRol { get; set; }

        public int idTipoAcceso { get; set; }

        public string nombreTipoAcceso { get; set; }
    }
}

