namespace Practica {
    internal class Ventana {

        private bool estaAbierta;

        public Ventana() {
            estaAbierta = false;
        }

        public void Abrir() { estaAbierta = true; }

        public void Cerrar() { estaAbierta = false; }

        public bool EstaAbierta {
            get { return this.estaAbierta; }
        }
    }
}
