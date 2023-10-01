namespace PracticaLibros {
    internal class Catalogo {

        List<Libro> libros;

        public Catalogo() {
            libros = new List<Libro>();
        }

        public void AgregarLibro(Libro libro) {
            if (libro == null)
                throw new ArgumentNullException("Se debe especificar un libro");

            if (BuscarLibroPorNombre(libro.Nombre) != null)
                throw new InvalidOperationException("El libro ya existe en el catálogo");

            libros.Add(libro);
        }

        public int NumeroDeLibros() {
            return libros.Count;
        }

        public Libro BuscarLibroPorNombre(string nombreABuscar) {
            return libros.FirstOrDefault(libro => libro.Nombre.Contains(nombreABuscar, StringComparison.OrdinalIgnoreCase));
        }

        public Libro BuscarLibroPorCantidadDeCaracteres(int cantidadCaracteres) {
            if (cantidadCaracteres < 1)
                throw new ArgumentException("Tiene que ser una cantidad de caracteres valida");
            return libros.FirstOrDefault(libro => libro.Nombre.Length == cantidadCaracteres);
        }

        public Libro BuscarLibroPorAnio(int anioABuscar) {
            if (anioABuscar < 1800)
                throw new ArgumentException("El año debe ser valido");
            return libros.FirstOrDefault(libro => libro.AnioDePublicacion == anioABuscar);
        }

        public bool EliminarLibro(string nombreAEliminar) {
            Libro libroEncontrado = BuscarLibroPorNombre(nombreAEliminar);
            if (libroEncontrado == null)
                return false;
            libros.Remove(libroEncontrado);
            return true;
        }

    }
}
