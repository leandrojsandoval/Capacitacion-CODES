using API_Cinema.Datos.Implementaciones;
using API_Cinema.Entidades;
using API_Cinema.Entidades.Filtros;
using API_Cinema.Framework.Common;
using System.Data.SqlClient;
using System.Data;

namespace API_Cinema.Servicios.Implementaciones {
    public class ServicioSucursal {

        DatosSucursal _datos;

        public ServicioSucursal(IConfiguration configuration) {
            _datos = new DatosSucursal(configuration);
        }

        public void Insertar(Sucursal sucursal) {
            _datos.Insertar(sucursal);
        }

        public void Actualizar(Sucursal sucursal) {
                _datos.Actualizar(sucursal);
        }

        public List<Sucursal> ObtenerSucursales() {
            return _datos.ObtenerSucursales();
        }

        public List<Sucursal> ObtenerSucursalPorId(int id) {
            return _datos.ObtenerSucursalPorId(id);
        }

    }
}
