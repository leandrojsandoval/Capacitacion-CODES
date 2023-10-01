namespace PracticaDTO {
    internal class Program {
        static void Main() {
            
            Persona persona = new() {
                Nombre = "Leandro",
                Apellido = "Sandoval",
                Edad = 24,
                FechaDeNacimiento = new DateTime(1998, 10, 22)
            };

            Console.WriteLine("Nombre: " + persona.Nombre);
            Console.WriteLine("Apellido: " + persona.Apellido);
            Console.WriteLine("Edad: " + persona.Edad);
            Console.WriteLine("Fecha de nacimiento: " + persona.FechaDeNacimiento.ToShortDateString());
        }
    }
}