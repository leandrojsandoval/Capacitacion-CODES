using ARQ.Datos.Interfaces;
using ARQ.Entidades;

namespace ARQ.Servicios.RelationshipValidators
{
    public abstract class RelationshipValidator<T> where T : EntidadBase
    {
        public abstract void Validate(T entidad, IDatosGenerico datosGenerico);
    }

}
 