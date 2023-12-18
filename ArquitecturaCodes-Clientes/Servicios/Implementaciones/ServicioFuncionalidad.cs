using ARQ.Datos.Interfaces;
using ARQ.Entidades;
using ARQ.Servicios.Implementaciones;
using ARQ.Servicios.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace ARQ.Servicios.Implementaciones
{
    public class ServicioFuncionalidad : ServicioGenerico, IServicioFuncionalidad
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IServicioGenerico _servicioGenerico { get; set; }

        public ServicioFuncionalidad(IDatosGenerico datos, IServicioGenerico servicioGenerico) : base(datos)
        {
            _servicioGenerico = servicioGenerico;
        }

        public void AgregarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAAgregar)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                this._datos.Add<Funcionalidad>(funcionalidad);

                if (funcionalidadesRolesAAgregar != null)
                {
                    foreach (FuncionalidadRol fr in funcionalidadesRolesAAgregar)
                    {
                        fr.IdFuncionalidad = funcionalidad.Id;
                        _servicioGenerico.Add<FuncionalidadRol>(fr);

                    }
                }
                scope.Complete();

            }
        }

        public void ModificarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAModificar)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                this._datos.Update(funcionalidad);

                List<FuncionalidadRol> funcRoles = _servicioGenerico.GetAll<FuncionalidadRol>(r => r.IdFuncionalidad == funcionalidad.Id).ToList();

                //roles a eliminar
                List<FuncionalidadRol> listaRelAEliminar = funcRoles.Where(re => !funcionalidadesRolesAModificar.Any(r => r.IdRol == re.IdRol && r.IdTipoAcceso == re.IdTipoAcceso)).ToList();
                listaRelAEliminar.ForEach(rol => _servicioGenerico.Delete<FuncionalidadRol>(rol));

                //roles a agregar
                List<FuncionalidadRol> listaRolesAAgregar = funcionalidadesRolesAModificar.Where(ra => !funcRoles.Any(r => r.IdRol == ra.IdRol && r.IdTipoAcceso == ra.IdTipoAcceso)).ToList();
                listaRolesAAgregar.ForEach(rol => _servicioGenerico.Add<FuncionalidadRol>(rol));

                scope.Complete();
            }
        }
    }
}
