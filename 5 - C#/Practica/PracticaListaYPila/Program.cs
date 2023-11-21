namespace PracticaListaYPila {
    internal class Program {
        static void Main() {

            List<string> listaLineas = [];
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
            while (listaLineas.Count != 0) {
                Console.WriteLine(listaLineas.Last());
                listaLineas.RemoveAt(listaLineas.Count - 1);
            }

            Console.WriteLine("Orden inverso - Pila");
            while (pilaLineas.Count != 0) {
                Console.WriteLine(pilaLineas.Pop());
            }
        }
    }
}
