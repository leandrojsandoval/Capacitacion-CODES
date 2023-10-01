namespace PracticaLibros {
    internal class Libro {

        private string nombre;
        private DateTime fechaDePublicacion;

        public Libro(string nombre, DateTime fechaDePublicacion) {
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException("El nombre del libro no puede estar vacío o nulo");
            this.nombre = nombre;
            this.fechaDePublicacion = fechaDePublicacion;
        }

        public string Nombre {
            get { return nombre; }
            set { nombre = value; }
        }

        public DateTime FechaDePublicacion {
            get { return fechaDePublicacion; }
            set { fechaDePublicacion = value; }
        }
        public int AnioDePublicacion {
            get { return fechaDePublicacion.Year; }
            set { fechaDePublicacion = new DateTime(value, fechaDePublicacion.Month, fechaDePublicacion.Day); }
        }

        public override string ToString() {
            return "Libro: " + nombre + ", Anio de publicacion: " + AnioDePublicacion;
        }

    }
}
