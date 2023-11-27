namespace Practica {
    internal class Puerta {

        private Ventana ventana;
        private bool estaAbierta;

        public Puerta() {
            this.ventana = new Ventana();
            this.estaAbierta = false;
        }

        public void Cerrar() { 
            this.estaAbierta = false; 
        }

        public void Abrir() {
            this.estaAbierta = true;
        }

        public bool EstaAbierta {
            get { return this.estaAbierta; }
        }
    }
}
