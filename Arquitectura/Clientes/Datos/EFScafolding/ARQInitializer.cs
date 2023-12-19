using ARQ.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARQ.Datos.EFScafolding {
    public static class ARQInitializer {

        const int USUARIO_MIGRACION = 1;
        public static void Initialize (ARQContext context) {

            if (context.Clientes.Any()) {
                return;   // DB has been seeded
            }

            List<Cliente> clientes = new List<Cliente>
            {
                new() {
                    Nombre = "Matias",
                    Apellido = "Gonzalez",
                    FechaNacimiento = new DateTime(2000, 10, 15),
                    // ESTOS DOS VALORES SON NECESARIOS YA QUE EN LA
                    // ENTIDAD BASE ESTOS ATRIBUTOS SON REQUIRED 
                    IdUsuarioAlta = USUARIO_MIGRACION,
                    FechaAlta = DateTime.Now,
                    // Y ESTE PARA QUE ACTIVO SE ENCUENTRA EN 1
                    Activo = true
                },
                new () {
                    Activo = true,
                    Nombre = "Alejandro",
                    Apellido = "Martínez",
                    FechaNacimiento = new DateTime(1990, 5, 10),
                    IdUsuarioAlta = USUARIO_MIGRACION,
                    FechaAlta = DateTime.Now
                },
                new () {
                    Activo = true,
                    Nombre = "Laura",
                    Apellido = "García",
                    FechaNacimiento = new DateTime(1985, 8, 25),
                    IdUsuarioAlta = USUARIO_MIGRACION,
                    FechaAlta = DateTime.Now
                },
                new () {
                    Activo = true,
                    Nombre = "Carlos",
                    Apellido = "Rodríguez",
                    FechaNacimiento = new DateTime(2002, 3, 15),
                    IdUsuarioAlta = USUARIO_MIGRACION,
                    FechaAlta = DateTime.Now
                },
            };

            foreach (Cliente cliente in clientes)
                context.Add<Cliente>(cliente);

            context.SaveChanges();
        }
    }
}