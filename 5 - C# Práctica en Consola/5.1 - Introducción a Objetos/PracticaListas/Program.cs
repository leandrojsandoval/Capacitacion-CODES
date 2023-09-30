namespace PracticaListas {
    internal class Program {
        static void Main(string[] args) {

            List<string> listaLineas = new();
            Stack<string> pilaLineas = new();
            string linea;

            Console.WriteLine("Ingrese 10 palabras");
            for (int i = 0; i < 10; i++) {
                do {
                    Console.Write("Palabra " + (i + 1) + ": ");
                    linea = Console.ReadLine();
                } while (string.IsNullOrEmpty(linea));
                listaLineas.Add(linea);
                pilaLineas.Push(linea);
            }

            Console.WriteLine("Orden inverso - Lista");
            while (listaLineas.Any()) {
                Console.WriteLine(listaLineas.Last());
                listaLineas.RemoveAt(listaLineas.Count - 1);
            }

            Console.WriteLine("Orden inverso - Pila");
            while(pilaLineas.Any()) {
                Console.WriteLine(pilaLineas.Pop());
            }
        }
    }
}