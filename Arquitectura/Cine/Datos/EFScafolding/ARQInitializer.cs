using ARQ.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARQ.Datos.EFScafolding {

    public static class ARQInitializer {

        const int USUARIO_MIGRACION = 1;

        public static void Initialize (ARQContext context) {

            if (context.Sucursales.Any()) {
                return;   // DB has been seeded
            }

            Usuario usuario1 = new() {
                Login = "eduardo99",
                Nombre = "Eduardo Vasquez",
                Email = "eduardo@kodoti.com",
                Activo = true,
                IdTurnoTrabajo = 1,
                IdUsuarioSGAA = 917
            };

            Pelicula pelicula1 = new() {
                Imagen = "/static/terminator.jpg",
                Nombre = "Terminator",
                Descripcion = "Un asesino cibernético del futuro es enviado a Los Ángeles, para matar a la mujer que procreará a un líder.",
                Activo = true,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now
            };
            Pelicula pelicula2 = new() {
                Imagen = "/static/dragonball.jpg",
                Nombre = "Terminator",
                Descripcion = "Dragon Ball Super: Broly', 'Después de disputarse el Torneo de la Fuerza, la Tierra se encuentra en paz. Goku al darse cuenta de que en el universo aún hay personas extremadamente fuertes, continúa entrenando para volverse más fuerte. Pero un día, Freezer aparece en la Tierra con un misterioso saiyajin llamado Broly, el cuál enfrenta a Goku y Vegeta.",
                Activo = true,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now
            };
            Pelicula pelicula3 = new() {
                Imagen = "/static/profesional.jpg",
                Nombre = "El perfecto asesino",
                Descripcion = "Mathilda es una niña que no se lleva bien con su familia, excepto con su hermano pequeño. Su padre es un narcotraficante que hace negocios con Stan, un corrupto agente de la D.E.A. Un día Stan mata a su familia y Mathilda se refugia en casa de Léon, un solitario y misterioso vecino que resulta ser un asesino a sueldo, pero hará un pacto con él: ella se encargará de las tareas domésticas y le enseñará a leer a Léon y éste le enseñará a disparar para poder vengarse de la muerte de su hermano.",
                Activo = true,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now
            };
            Pelicula pelicula4 = new() {
                Imagen = "/static/johnwick.jpg",
                Nombre = "John Wick",
                Descripcion = "John Wick es una película de acción estadounidense de 2014, dirigida por David Leitch y Chad Stahelski.",
                Activo = true,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now
            };

            Sucursal sucursal1 = new() {
                Activo = true,
                FechaAlta = DateTime.Now,
                IdUsuarioAlta = USUARIO_MIGRACION,
                Nombre = "KODOTI Stars Zona Norte",
                Precio = 17,
            };
            Sucursal sucursal2 = new() {
                Activo = true,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now,
                Nombre = "KODOTI Stars Zona Sur",
                Precio = 18,
            };
            Sucursal sucursal3 = new() {
                Activo = true,
                FechaAlta = DateTime.Now,
                IdUsuarioAlta = USUARIO_MIGRACION,
                Nombre = "KODOTI Stars Zona Este",
                Precio = 24,
            };
            Sucursal sucursal4 = new() {
                Activo = true,
                FechaAlta = DateTime.Now,
                IdUsuarioAlta = USUARIO_MIGRACION,
                Nombre = "KODOTI Stars Zona Oeste",
                Precio = 21.5,
            };

            List<Horario> horarios = new() {
                new() { Pelicula = pelicula1, Sucursal = sucursal1, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal3, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal1, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal2, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal3, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal4, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal1, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal2, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal3, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal4, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal2, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal3, Hora = "12:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal1, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal2, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal4, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal1, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal2, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal3, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal4, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal1, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal2, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal3, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal4, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal1, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal2, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal3, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal4, Hora = "14:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal1, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal2, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal3, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal1, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal2, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal3, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal1, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal2, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal3, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal4, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal1, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal2, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal3, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal4, Hora = "16:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal1, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal2, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal3, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal4, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal1, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal2, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal3, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal4, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal1, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal3, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal1, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal3, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal4, Hora = "18:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal1, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal2, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula1, Sucursal = sucursal4, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula2, Sucursal = sucursal3, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal1, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal2, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal3, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula3, Sucursal = sucursal4, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal1, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal2, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal3, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now },
                new() { Pelicula = pelicula4, Sucursal = sucursal4, Hora = "20:00:00", Activo = true, IdUsuarioAlta = USUARIO_MIGRACION, FechaAlta = DateTime.Now }
            };

            Orden orden = new() {
                Usuario = usuario1,
                Horario = horarios.First(),
                Cantidad = 4,
                Total = 17,
                Activo = true,
                IdUsuarioAlta = USUARIO_MIGRACION,
                FechaAlta = DateTime.Now
            };

            context.Add<Usuario>(usuario1);

            context.Add<Pelicula>(pelicula1);
            context.Add<Pelicula>(pelicula2);
            context.Add<Pelicula>(pelicula3);
            context.Add<Pelicula>(pelicula4);

            context.Add<Sucursal>(sucursal1);
            context.Add<Sucursal>(sucursal2);
            context.Add<Sucursal>(sucursal3);
            context.Add<Sucursal>(sucursal4);

            foreach (Horario horario in horarios) {
                context.Add<Horario>(horario);
            }

            context.Add<Orden>(orden);

            context.SaveChanges();

        }

    }

}
