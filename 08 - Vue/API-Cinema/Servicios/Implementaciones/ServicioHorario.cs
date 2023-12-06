using API_Cinema.Datos.Implementaciones;
using API_Cinema.Entidades;

namespace API_Cinema.Servicios.Implementaciones {
    public class ServicioHorario {

        DatosHorario _datos;

        public ServicioHorario(IConfiguration configuration) {
            _datos = new DatosHorario(configuration);
        }

        public void Insertar(Horario horario) {
            _datos.Insertar(horario);
        }
    }
}
