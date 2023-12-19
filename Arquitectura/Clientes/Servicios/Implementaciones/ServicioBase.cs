﻿using Servicios.Interfaces;
using ARQ.Datos.Interfaces;

namespace Servicios.Implementaciones
{
    public class ServicioBase<TDatos>  : IServicioBase
        where TDatos : IDatosBase
    {
        protected readonly TDatos _datos;
        public ServicioBase(TDatos datos)
        {
            _datos = datos;
        }
    }
}
