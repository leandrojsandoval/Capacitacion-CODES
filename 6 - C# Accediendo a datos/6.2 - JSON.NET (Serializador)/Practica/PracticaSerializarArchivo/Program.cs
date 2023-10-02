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

            string json = JsonConvert.SerializeObject(persona, Formatting.Indented);
            File.WriteAllText(@"Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + ""\\JsonSerializado.json""", JsonConvert.SerializeObject(json));
            Console.WriteLine(json);
        }
    }
}