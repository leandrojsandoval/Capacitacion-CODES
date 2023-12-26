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

            List<Cliente> clientes = new List<Cliente> {
                new() {
                    Nombre = "Matias",
                    Apellido = "Gonzalez",
                    FechaNacimiento = new DateTime(2000, 10, 15),
                    IdUsuarioAlta = USUARIO_MIGRACION,
                    FechaAlta = DateTime.Now,
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

            Producto manzana = new() {
                Nombre = "Manzana",
                Descripcion = "Manzana Roja",
                Precio = 250,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now,
                Activo = true
            };

            Producto banana = new() {
                Nombre = "Banana",
                Descripcion = "Banana Ecuador",
                Precio = 700,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now,
                Activo = true
            };

            Producto naranja = new() {
                Nombre = "Naranja",
                Descripcion = "Naranja Ombligo",
                Precio = 700,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now,
                Activo = true
            };

            Venta ventaFrutas = new() {
                Descripcion = "Venta a cliente por unidad",
                Fecha = DateTime.Now,
                Cliente = clientes.First(),
                Productos = new() { manzana, banana, naranja },
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now,
                Activo = true
            };

            Venta ventaManzana = new() {
                Descripcion = "Venta a cliente por mayor",
                Fecha = DateTime.Now,
                Cliente = clientes[3],
                Productos = new() { manzana },
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now,
                Activo = true
            };

            context.Add<Producto>(manzana);
            context.Add<Producto>(banana);
            context.Add<Producto>(naranja);

            context.Add<Venta>(ventaFrutas);
            context.Add<Venta>(ventaManzana);

            context.SaveChanges();

        }

    }

}
