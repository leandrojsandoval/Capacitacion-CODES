namespace PracticaListas {
    public class Persona {

        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public override string ToString() {
            return string.Format("{0,-15} {1,-15} {2,-5} {3}", Nombre, Apellido, Edad, FechaNacimiento.ToShortDateString());
        }

    }
}
