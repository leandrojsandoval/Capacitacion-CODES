using System;
using System.Runtime.Serialization;

namespace PracticaBaseDeDatos {
    [Serializable]
    internal class DniInvalidoException : Exception {
        public DniInvalidoException() {
        }

        public DniInvalidoException(string message) : base(message) {
        }

        public DniInvalidoException(string message, Exception innerException) : base(message, innerException) {
        }

        protected DniInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}