namespace Practica {
    internal class Complejo {

        private float parteReal;
        private float parteImaginaria;

        public Complejo() {
            this.parteReal = 0f;
            this.parteImaginaria = 0f;
        }

        public Complejo(float parteReal, float parteImaginaria) {
            this.parteReal = parteReal;
            this.parteImaginaria = parteImaginaria;
        }

        public Complejo Sumar(Complejo numeroComplejo) {
            return new Complejo(this.parteReal + numeroComplejo.parteReal, this.parteImaginaria + numeroComplejo.parteImaginaria);
        }

        public Complejo Restar(Complejo numeroComplejo) {
            return new Complejo(this.parteReal - numeroComplejo.parteReal, this.parteImaginaria - numeroComplejo.parteImaginaria);
        }

        public Complejo Multiplicar(float numero) {
            return new Complejo(this.parteReal * numero, this.parteImaginaria * numero);
        }

        public Complejo Multiplicar(Complejo numeroComplejo) {
            return new Complejo(this.parteReal * numeroComplejo.parteReal - this.parteImaginaria * numeroComplejo.parteImaginaria, this.parteReal * numeroComplejo.parteImaginaria + this.parteImaginaria * numeroComplejo.parteReal);
        }

        public bool EsIgualA (Complejo numeroComplejo) {
            return this.parteReal == numeroComplejo.parteReal && this.parteImaginaria == numeroComplejo.parteImaginaria;
        }

        public override string ToString() {
            return this.parteReal + " + " + this.parteImaginaria + "i";
        }

        public float ParteReal {
            get { return this.parteReal; }
            set { this.parteImaginaria = value; }
        }
        public float ParteImaginaria {
            get { return this.parteImaginaria; }
            set { this.parteImaginaria = value; }
        }

    }
}
