using ARQ.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARQ.Datos.EFScafolding
{
    public static class ARQInitializer
    {
        const int USUARIO_MIGRACION = 1;
        public static void Initialize(ARQContext context)
        {
            
            context.SaveChanges();
        }
    }
}