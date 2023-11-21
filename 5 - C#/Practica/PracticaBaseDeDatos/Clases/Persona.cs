using System;
using System.ComponentModel.DataAnnotations;

namespace PracticaBaseDeDatos {
    public class Persona {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }

        public string Dni { get; set; }

        public string Email { get; set; }

        public Persona() { }

        public Persona(string nombre, string apellido, int edad, string dni, string email) {
            if (!EsDNIValido(dni))
                throw new DniInvalidoException();
            if (!EsEmailValido(email))
                throw new EmailInvalidoException();
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Dni = dni;
            Email = email;
        }

        bool EsDNIValido(string dni) {
            if (dni.Length != 8) {
                return false;
            }
            foreach (char digito in dni) {
                if (!Char.IsDigit(digito)) {
                    return false;
                }
            }
            return true;
        }

        bool EsEmailValido(string email) {
            int indiceArroba = email.IndexOf("@");
            if (indiceArroba == -1)
                return false;
            string dominio = email.Substring(indiceArroba);
            if (!dominio.Equals("@hotmail.com", StringComparison.OrdinalIgnoreCase) && !dominio.Equals("@gmail.com", StringComparison.OrdinalIgnoreCase))
                return false;
            return true;
        }
    }
}
