using ARQ.Entidades;
using ARQ.Datos.Interfaces;
using ARQ.Servicios.Interfaces;
using Servicios.Implementaciones;

namespace ARQ.Servicios.Implementaciones
{
    public class ServicioMaterial : ServicioBase<IDatosMaterial>, IServicioMaterial
    {
        private IServicioGenerico _servicioGenerico { get; set; }

        public ServicioMaterial(IDatosGenerico datos, IServicioGenerico servicioGenerico) : base(datos)
        {
            _servicioGenerico = servicioGenerico;
        }

        public Material GetById(int idMaterial)
        {
            
        }
    }
}
