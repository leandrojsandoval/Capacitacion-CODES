using System;
using System.Runtime.Serialization;

namespace PracticaBaseDeDatos {
    [Serializable]
    internal class EmailInvalidoException : Exception {
        public EmailInvalidoException() {
        }

        public EmailInvalidoException(string message) : base(message) {
        }

        public EmailInvalidoException(string message, Exception innerException) : base(message, innerException) {
        }

        protected EmailInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}