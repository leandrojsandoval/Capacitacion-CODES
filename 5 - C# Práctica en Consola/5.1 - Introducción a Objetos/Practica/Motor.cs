namespace Practica {
    internal class Motor {

        private bool estaPrendido;

        public Motor() {
            this.estaPrendido = false;
        }

        public void Arrancar() {
            this.estaPrendido = true;
        }

        public void Apagar() {
            this.estaPrendido = false;
        }

        public bool EstaPrendido {
            get { return this.estaPrendido; }
        }

    }
}
