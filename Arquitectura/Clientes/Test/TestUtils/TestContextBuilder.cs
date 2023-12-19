using Servicios.Interfaces;
using ARQ.Datos.EFScafolding;
using ARQ.Datos.Implementacion;
using Servicios.Implementaciones;
using Microsoft.EntityFrameworkCore;
using ARQ.Servicios.Interfaces;
using ARQ.Servicios.Implementaciones;

namespace ARQ.Test
{
    public static class TestContextBuilder
    {
        public static ARQContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ARQContext>()
                            .UseInMemoryDatabase(databaseName: "ARQContext")
                            .Options;

            return new ARQContext(options);
        }

        public static ARQContext GetLocalContext()
        {
            var options = new DbContextOptionsBuilder<ARQContext>()
                            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RollShop;Trusted_Connection=True;MultipleActiveResultSets=true;")
                            .Options;

            return new ARQContext(options);
        }

       
        public static IServicioGenerico GetServicioGenerico(ARQContext context)
        {
            DatosGenerico datos = new DatosGenerico(context);
            ServicioGenerico servicio = new ServicioGenerico(datos);

            return servicio;
        }
    }
}
