namespace Practica {
    internal class Rueda {

        private bool estaInflada;

        public Rueda() {
            this.estaInflada = true;
        }

        public void Inflar() {
            this.estaInflada = true;
        }

        public void Desinflar() {
            this.estaInflada = false;
        }

        public bool EstaInflada {
            get { return this.estaInflada; }
        }
    }
}
