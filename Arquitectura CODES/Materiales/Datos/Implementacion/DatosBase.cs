using log4net;
using ARQ.Datos.Interfaces;
using System.Reflection;

namespace ARQ.Datos.EFScafolding {

    /* Clase abstracta que implementa la interfaz IDatosBase 
     
    Log4net Integration: Utiliza Log4net para la creación de un objeto de registro (ILog). 
    Este objeto se inicializa con el tipo que está declarando la clase 
    (obtenido mediante MethodBase.GetCurrentMethod().DeclaringType). 
    Esto se utiliza para registrar mensajes dentro de la clase derivada.
    
    https://logging.apache.org/log4net/
    
    Al heredar de esta clase, las clases derivadas obtienen acceso al objeto de registro 
    de Log4net (log) y al contexto de la base de datos (_context). 
    La inclusión de Log4net sugiere que la clase derivada puede registrar 
    información, advertencias o errores utilizando Log4net. */

    public abstract class DatosBase : IDatosBase {

        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly ARQContext _context;

        //Constructor
        public DatosBase (ARQContext context) {
            this._context = context;
        }

    }

}
