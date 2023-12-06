using API_Cinema.Datos.Implementaciones;
using API_Cinema.Entidades;

namespace API_Cinema.Servicios.Implementaciones {
    public class ServicioVenta {

        DatosVenta _datos;

        public ServicioVenta(IConfiguration configuration) {
            _datos = new DatosVenta(configuration);
        }

        public List<Venta> ObtenerVentas() {
            return _datos.ObtenerVentas();
        }

        public void Insertar(Venta venta) {
            _datos.Insertar(venta);
        }

    }
}
