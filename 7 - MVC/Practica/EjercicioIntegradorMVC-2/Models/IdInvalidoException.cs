using System;
using System.Runtime.Serialization;

namespace EjercicioIntegradorMVC_2.Controllers {
    [Serializable]
    internal class IdInvalidoException : Exception {
        public IdInvalidoException() {
        }

        public IdInvalidoException(string message) : base(message) {
        }

        public IdInvalidoException(string message, Exception innerException) : base(message, innerException) {
        }

        protected IdInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}