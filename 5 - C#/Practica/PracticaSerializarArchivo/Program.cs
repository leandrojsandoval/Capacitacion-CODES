using Newtonsoft.Json;

namespace PracticaSerializarArchivo {
    internal class Program {
        static void Main ()
        {
            Persona persona = new()
            {
                Nombre = "Leandro",
                Apellido = "Sandoval",
                Edad = 24,
                FechaNacimiento = new DateTime(1998, 10, 22)
            };

            // Primero serializo el objeto y luego escribo en el archivo
            string json = JsonConvert.SerializeObject(persona, Formatting.Indented);
            File.WriteAllText("JsonSerializado.json", JsonConvert.SerializeObject(json));
            Console.WriteLine(json);

            // Los datos JSON se escriben directamente en el archivo
            using (StreamWriter archivo = File.CreateText("JsonConSerializador.json")) {
                JsonSerializer serializador = new JsonSerializer();
                serializador.Serialize(archivo, json);
            }
        }
    }
}