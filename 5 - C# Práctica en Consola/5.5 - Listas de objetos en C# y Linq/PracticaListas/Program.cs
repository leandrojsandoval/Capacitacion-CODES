namespace PracticaListas {
    internal class Program {
        static void Main() {
            var listaPersona = new List<Persona>();
            listaPersona.AddRange(new List<Persona>
            {
                new Persona()
                {
                    Nombre = "Leandro",
                    Apellido = "Sandoval",
                    Edad = 24,
                    FechaNacimiento = new DateTime(1998, 10, 22)
                },
                new Persona()
                {
                    Nombre = "María",
                    Apellido = "González",
                    Edad = 30,
                    FechaNacimiento = new DateTime(1992, 5, 15)
                },
                new Persona()
                {
                    Nombre = "Carlos",
                    Apellido = "Martínez",
                    Edad = 45,
                    FechaNacimiento = new DateTime(1978, 8, 10)
                },
                new Persona()
                {
                    Nombre = "Laura",
                    Apellido = "Pérez",
                    Edad = 28,
                    FechaNacimiento = new DateTime(1995, 3, 8)
                },
                new Persona()
                {
                    Nombre = "Pedro",
                    Apellido = "López",
                    Edad = 35,
                    FechaNacimiento = new DateTime(1986, 7, 20)
                },
                new Persona()
                {
                    Nombre = "Ana",
                    Apellido = "Rodríguez",
                    Edad = 22,
                    FechaNacimiento = new DateTime(2001, 12, 5)
                },
                new Persona()
                {
                    Nombre = "Luis",
                    Apellido = "Fernández",
                    Edad = 17,
                    FechaNacimiento = new DateTime(2006, 2, 14)
                },
                new Persona()
                {
                    Nombre = "Sofía",
                    Apellido = "Díaz",
                    Edad = 29,
                    FechaNacimiento = new DateTime(1993, 9, 30)
                },
                new Persona()
                {
                    Nombre = "Eduardo",
                    Apellido = "Ramírez",
                    Edad = 38,
                    FechaNacimiento = new DateTime(1984, 11, 11)
                },
                new Persona()
                {
                    Nombre = "Isabel",
                    Apellido = "Gómez",
                    Edad = 16,
                    FechaNacimiento = new DateTime(2005, 6, 25)
                }
            });

            Console.WriteLine("LISTA ORIGINAL");
            MostrarListaPersonas(listaPersona);
            Console.WriteLine("\n");

            Console.WriteLine("LISTA ORDENADA POR NOMBRE");
            var listaOrdenada = listaPersona.OrderBy(persona => persona.Nombre).ToList();
            MostrarListaPersonas(listaOrdenada);
            Console.WriteLine("\n");

            Console.WriteLine("LISTA FILTRADA POR MAYOR DE EDAD");
            var listaFiltrada = listaPersona.Where(persona => persona.Edad > 18).ToList();
            MostrarListaPersonas(listaFiltrada);

        }

        static void MostrarListaPersonas(List<Persona> lista) {
            Console.WriteLine(string.Format("{0,-15} {1,-15} {2,-5} {3}", "Nombre", "Apellido", "Edad", "Fecha de nacimiento"));
            foreach (var Persona in lista) {
                Console.WriteLine(Persona);
            }
        }

    }
}