namespace Practica {
    internal class Coche {

        private Motor motor;
        private Rueda[] ruedas;
        private Puerta[] puertas;

        public Coche() {
            motor = new Motor();
            ruedas = new Rueda[4];
            puertas = new Puerta[2];

            for (int i = 0; i < 4; i++) {
                ruedas[i] = new Rueda();
            }

            for (int i = 0; i < 2; i++) {
                puertas[i] = new Puerta();
            }
        }

        public Coche(Motor motor, Rueda[] ruedas, Puerta[] puertas) {
            if (ruedas.Length != 4)
                throw new Exception("Las ruedas deberian ser 4");

            if (puertas.Length != 2)
                throw new Exception("Se necesitan un par de puertas");

            this.motor = motor;
            this.ruedas = ruedas;
            this.puertas = puertas;
        }

        public void Estado() {
            System.Console.WriteLine("Motor: " + (motor.EstaPrendido ? "Encendido" : "Apagado"));
            for (int i = 0; i < 4; i++) {
                System.Console.WriteLine("Rueda" + (i + 1) + ": " + (ruedas[i].EstaInflada ? "Inflada" : "Desinflada"));
            }
            for (int i = 0; i < 2; i++) {
                System.Console.WriteLine("Puerta" + (i + 1) + ": " + (puertas[i].EstaAbierta ? "Abierta" : "Cerrada"));
            }
        }

        public Motor Motor { get { return motor; } }

        public Puerta[] Puertas { get { return puertas; } }

        public Rueda[] Ruedas { get { return ruedas; } }

    }
}
