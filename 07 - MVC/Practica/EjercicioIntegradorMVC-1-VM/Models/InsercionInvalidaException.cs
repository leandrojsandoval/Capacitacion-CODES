using System;
using System.Runtime.Serialization;

namespace EjercicioIntegradorMVC_1_VM.Models {
    [Serializable]
    internal class InsercionInvalidaException : Exception {
        public InsercionInvalidaException() {
        }

        public InsercionInvalidaException(string message) : base(message) {
        }

        public InsercionInvalidaException(string message, Exception innerException) : base(message, innerException) {
        }

        protected InsercionInvalidaException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}