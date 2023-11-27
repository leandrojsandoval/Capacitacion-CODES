namespace PracticaAplicacionEnConsola {
    internal class Program {
        static void Main(string[] args) {

            int numeroEntero = 10;
            double numeroDecimal = 5.75;
            string nombre = "Leandro Sandoval";
            bool esVerdadero = true;
            char caracter = 'S';
            
            Console.WriteLine("Numero entero: " + numeroEntero);
            Console.WriteLine("Numero decimal: " + numeroDecimal);
            Console.WriteLine("Nombre: " + nombre);
            Console.WriteLine("Es verdadero? " + (esVerdadero? "Si" : "No"));
            Console.WriteLine("Caracter: " + caracter);
        }
    }
}