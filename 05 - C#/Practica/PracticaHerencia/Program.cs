namespace PracticaHerencia {
    internal class Program {
        static void Main() {
            Lampara lampara = new();
            Linterna linterna = new();
            FarolLed farol = new ();
            
            lampara.Conectar();
            linterna.Usar();
            farol.Conectar();
            farol.Cargar();
        }
    }
}