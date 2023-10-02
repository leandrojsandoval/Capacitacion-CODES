using Newtonsoft.Json;

namespace PracticaDeserializarObjeto {
    internal class Program {
        static void Main() {
            string json = @"{
              'Nombre': 'Leandro',
              'Apellido': 'Sandoval',
              'Edad': '24',
              'FechaNacimiento': '1998-10-22T00:00:00',
            }";
            
            Persona persona = JsonConvert.DeserializeObject<Persona>(json);
            
            if (persona == null )
                throw new NullReferenceException("No se pudo deserializar el objeto");
            
            Console.WriteLine("Objeto deserializado");
            Console.WriteLine(persona.Nombre);
            Console.WriteLine(persona.Apellido);
            Console.WriteLine(persona.Edad);
            Console.WriteLine(persona.FechaNacimiento);
        }
    }
}