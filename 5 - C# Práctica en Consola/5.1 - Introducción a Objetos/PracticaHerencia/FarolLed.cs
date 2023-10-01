namespace PracticaHerencia {
    internal class FarolLed : Enchufable, Recargable {
        public void Cargar() {
            Console.WriteLine("Cargando el farol mediante su bateria interna.");
        }

        public void Conectar() {
            Console.WriteLine("Conectando el farol al enchufe.");
        }
    }
}
