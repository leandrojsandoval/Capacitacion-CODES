using ARQ.Entidades;
using System.Collections.Generic;

namespace ARQ.Servicios.Interfaces
{
    public interface IServicioFuncionalidad : IServicioGenerico
    {
        public void AgregarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAModificar);

        public void ModificarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAModificar);
    }
}