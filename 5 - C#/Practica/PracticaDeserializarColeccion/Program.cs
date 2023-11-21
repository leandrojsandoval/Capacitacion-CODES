using Newtonsoft.Json;

namespace PracticaDeserializarColeccion {
    internal class Program {
        static void Main() {
            string json = @"[
              {
                ""Nombre"": ""Leandro"",
                ""Apellido"": ""Sandoval"",
                ""Edad"": 24,
                ""FechaNacimiento"": ""1998-10-22T00:00:00""
              },
              {
                ""Nombre"": ""María"",
                ""Apellido"": ""González"",
                ""Edad"": 30,
                ""FechaNacimiento"": ""1992-05-15T00:00:00""
              },
              {
                ""Nombre"": ""Carlos"",
                ""Apellido"": ""Martínez"",
                ""Edad"": 45,
                ""FechaNacimiento"": ""1978-08-10T00:00:00""
              },
              {
                ""Nombre"": ""Laura"",
                ""Apellido"": ""Pérez"",
                ""Edad"": 28,
                ""FechaNacimiento"": ""1995-03-08T00:00:00""
              },
              {
                ""Nombre"": ""Pedro"",
                ""Apellido"": ""López"",
                ""Edad"": 35,
                ""FechaNacimiento"": ""1986-07-20T00:00:00""
              }
            ]";

            try {
                List<Persona> listaPersonas = JsonConvert.DeserializeObject<List<Persona>>(json);
                Console.WriteLine(string.Format("{0,-15} {1,-15} {2,-5} {3}", "Nombre", "Apellido", "Edad", "Fecha de nacimiento"));
                foreach (var Persona in listaPersonas) {
                    Console.WriteLine(Persona);
                }
            }
            catch (JsonSerializationException exception) {
                Console.WriteLine("Error al deserializar el JSON: " + exception.Message);
            }
        }
    }
}