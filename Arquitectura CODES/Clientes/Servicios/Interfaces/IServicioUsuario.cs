using ARQ.Entidades;
using Servicios.Interfaces;

namespace ARQ.Servicios.Interfaces
{
    public interface IServicioUsuario : IServicioBase
    {
        void Update<T>(Usuario usuario) where T : class;
    }
}
