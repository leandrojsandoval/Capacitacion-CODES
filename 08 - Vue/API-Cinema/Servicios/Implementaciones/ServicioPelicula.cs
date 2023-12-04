using API_Cinema.Datos.Implementaciones;
using API_Cinema.Entidades;

namespace API_Cinema.Servicios.Implementaciones {
    public class ServicioPelicula {

        DatosPelicula _datos;

        public ServicioPelicula(IConfiguration configuration) {
            _datos = new DatosPelicula(configuration);
        }

        public List<Pelicula> ObtenerPeliculas() {
            return _datos.ObtenerPeliculas();
        }

        public Pelicula ObtenerPeliculaPorId(int id) {
            return _datos.ObtenerPeliculaPorId(id);
        }

    }

}
