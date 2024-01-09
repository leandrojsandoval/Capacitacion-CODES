using ARQ.Datos.EFScafolding;
using ARQ.Datos.Implementacion;
using ARQ.Servicios.Implementaciones;
using ARQ.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ARQ.Test
{

    public static class TestContextBuilder
    {
        public static ARQContext GetMemoryContext ()
        {
            var options = new DbContextOptionsBuilder<ARQContext>()
                            .UseInMemoryDatabase(databaseName: "ARQContext")
                            .Options;
            return new ARQContext(options);
        }

        public static ARQContext GetLocalContext ()
        {
            var options = new DbContextOptionsBuilder<ARQContext>()
                            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ARQCODES;Trusted_Connection=True;MultipleActiveResultSets=true;")
                            .Options;
            return new ARQContext(options);
        }

        public static IServicioGenerico GetServicioGenerico (ARQContext context)
        {
            DatosGenerico datos = new(context);
            ServicioGenerico servicio = new(datos);
            return servicio;
        }

    }

}
