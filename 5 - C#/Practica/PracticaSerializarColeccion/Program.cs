using Newtonsoft.Json;

namespace PracticaSerializarColeccion {
    internal class Program {
        static void Main ()
        {
            List<Persona> listaPersona = new List<Persona>
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
                }
            };
            string json = JsonConvert.SerializeObject(listaPersona, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}