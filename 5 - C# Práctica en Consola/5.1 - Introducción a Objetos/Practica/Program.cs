namespace Practica {

    internal class Program {
        static void Main(string[] args) {
            Complejo numeroComplejo = new Complejo(5, 2);
            Racional numeroRacional = new Racional(5, 2);
            Console.WriteLine(numeroComplejo);
            Console.WriteLine(numeroRacional);

            Coche coche = new Coche();

            Console.WriteLine();
            Console.WriteLine("Estado de inicializacion");
            coche.Estado();

            coche.Motor.Arrancar();
            coche.Ruedas[2].Desinflar();
            coche.Puertas[0].Abrir();

            Console.WriteLine();
            Console.WriteLine("Estado despues de los metodos");
            coche.Estado();
        }
    }
}