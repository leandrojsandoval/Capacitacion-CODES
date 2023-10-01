namespace PracticaLibros {
    internal class Program {
        static void Main() {
            Libro elGranGatsy = new("El Gran Gatsby", new DateTime(1925, 4, 10));
            Libro matarAUnRuisenior = new("Matar a un ruiseñor", new DateTime(1960, 7, 11));
            Libro harryPotterYLaPiedraFilosofal = new("Harry Potter y la piedra filosofal", new DateTime(1997, 6, 26));

            matarAUnRuisenior.AnioDePublicacion = 2020;

            Catalogo catalogo = new();
            catalogo.AgregarLibro(elGranGatsy);
            catalogo.AgregarLibro(matarAUnRuisenior);
            catalogo.AgregarLibro(harryPotterYLaPiedraFilosofal);

            Console.WriteLine(catalogo.BuscarLibroPorNombre("potter"));

            Libro libroEncontrado = catalogo.BuscarLibroPorCantidadDeCaracteres(harryPotterYLaPiedraFilosofal.Nombre.Length);

            Console.WriteLine(libroEncontrado != null ? "El libro es: " + libroEncontrado.Nombre : "Los caracteres ingresados no coinciden con ningun libro de la lista.");
        
            Console.WriteLine(catalogo.BuscarLibroPorAnio(2020));
        }
    }
}