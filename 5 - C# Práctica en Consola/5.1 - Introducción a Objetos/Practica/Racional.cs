namespace Practica {

    internal class Racional {
        private float numerador;
        private float denominador;

        public Racional (float numerador, float denominador) {
            if(denominador == 0) {
                throw new ArgumentException("El denominador no puede ser 0");
            }
            this.numerador = numerador;
            this.denominador = denominador;
        }

        public Racional Sumar(Racional numeroRacional) {
            return new Racional(this.numerador * numeroRacional.denominador + this.denominador * numeroRacional.numerador, this.denominador * numeroRacional.denominador);
        }

        public Racional Multiplicar(Racional numeroRacional) {
            return new Racional(this.numerador * numeroRacional.numerador, this.denominador * numeroRacional.denominador);
        }

        public override string ToString() {
            return this.numerador + "/" + this.denominador;
        }

    }
}
