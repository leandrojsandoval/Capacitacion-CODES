using System;
using System.Runtime.Serialization;

namespace EjercicioIntegradorMVC_1_VM.Controllers {
    [Serializable]
    internal class ModificacionInvalidaException : Exception {
        public ModificacionInvalidaException ()
        {
        }

        public ModificacionInvalidaException (string message) : base(message)
        {
        }

        public ModificacionInvalidaException (string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModificacionInvalidaException (SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}