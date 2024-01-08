using ARQ.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARQ.Datos.EFScafolding {

    public static class ARQInitializer {

        const int USUARIO_MIGRACION = 1;

        public static void Initialize (ARQContext context) {

            if (context.Materiales.Any()) {
                return;   // DB has been seeded
            }

            Producto producto1 = new() {
                Activo = true,
                Descripcion = "Producto Test 1",
                FechaAlta = DateTime.Now,
                Nombre = "Producto 1",
                IdUsuarioAlta = USUARIO_MIGRACION,
                Materiales = new List<Material>()
                {
                    new()
                    {
                        Activo = true,
                        Descripcion = "Material Test 1",
                        FechaAlta = DateTime.Now ,
                        Nombre = "Material 1" ,
                        IdUsuarioAlta = USUARIO_MIGRACION
                    },
                    new()
                    {
                        Activo = true,
                        Descripcion = "Material Test 2",
                        FechaAlta = DateTime.Now,
                        Nombre = "Material 2",
                        IdUsuarioAlta = USUARIO_MIGRACION
                    }
                }
            };

            Producto producto2 = new() {
                Activo = true,
                Descripcion = "Producto Test 2",
                FechaAlta = DateTime.Now,
                Nombre = "Producto 2",
                IdUsuarioAlta = USUARIO_MIGRACION,
                Materiales = new List<Material>()
                {
                    new()
                    {
                        Activo = true,
                        Descripcion = "Material Test 3",
                        FechaAlta = DateTime.Now ,
                        Nombre = "Material 3" ,
                        IdUsuarioAlta = USUARIO_MIGRACION
                    },
                    new()
                    {
                        Activo = true,
                        Descripcion = "Material Test 4",
                        FechaAlta = DateTime.Now,
                        Nombre = "Material 4",
                        IdUsuarioAlta = USUARIO_MIGRACION
                    },
                    new()
                    {
                        Activo = true,
                        Descripcion = "Material Test 5",
                        FechaAlta = DateTime.Now,
                        Nombre = "Material 5",
                        IdUsuarioAlta = USUARIO_MIGRACION
                    }
                }
            };

            Producto producto3 = new() {
                Activo = true,
                Descripcion = "Producto Test 3",
                FechaAlta = DateTime.Now,
                Nombre = "Producto 3",
                IdUsuarioAlta = USUARIO_MIGRACION,
                Materiales = new List<Material>()
                {
                    new()
                    {
                        Activo = true,
                        Descripcion = "Material Test 6",
                        FechaAlta = DateTime.Now ,
                        Nombre = "Material 6" ,
                        IdUsuarioAlta = USUARIO_MIGRACION
                    },
                    new()
                    {
                        Activo = true,
                        Descripcion = "Material Test 7",
                        FechaAlta = DateTime.Now ,
                        Nombre = "Material 7" ,
                        IdUsuarioAlta = USUARIO_MIGRACION
                    }
                }
            };

            context.Add<Producto>(producto1);
            context.Add<Producto>(producto2);
            context.Add<Producto>(producto3);

            context.SaveChanges();
        }
    }
}