using log4net;
using ARQ.Datos.Interfaces;
using System.Reflection;

namespace ARQ.Datos.EFScafolding
{
    public abstract class DatosBase : IDatosBase
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly ARQContext _context;

        //Constructor
        public DatosBase(ARQContext context)
        {
            this._context = context;
        }       
    }
}
