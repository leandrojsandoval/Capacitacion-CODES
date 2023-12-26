using ARQ.Datos.Interfaces;
using ARQ.Entidades;
using ARQ.Servicios.Interfaces;
using Servicios.Implementaciones;

namespace ARQ.Servicios.Implementaciones
{
    public class ServicioUsuario : ServicioBase<IDatosGenerico>, IServicioUsuario
    {
        public ServicioUsuario(IDatosGenerico datos) : base(datos) { }

        public void Update<T>(Usuario usuario) where T : class
        {
            _datos.Update(usuario);
        }
    }
}
