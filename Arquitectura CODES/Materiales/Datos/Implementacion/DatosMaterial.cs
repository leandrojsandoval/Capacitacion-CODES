using ARQ.Datos.EFScafolding;
using ARQ.Datos.Interfaces;

namespace ARQ.Datos.Implementacion
{
    public class DatosMaterial : DatosBase, IDatosMaterial
    {
        public DatosMaterial(ARQContext context) : base(context) { }
    }
}
