namespace Practica {

    internal class Program {
        static void Main() {
            Complejo numeroComplejo = new(5, 2);
            Racional numeroRacional = new(5, 2);
            Console.WriteLine(numeroComplejo);
            Console.WriteLine(numeroRacional);

            Coche coche = new();

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