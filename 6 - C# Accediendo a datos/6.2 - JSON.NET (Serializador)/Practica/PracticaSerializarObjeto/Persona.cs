using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaSerializarObjeto {
    internal class Persona {

        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
