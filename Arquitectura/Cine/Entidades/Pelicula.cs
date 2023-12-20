using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_PELICULAS")]
    public class Pelicula : EntidadBase {

        [Column(name:"ID_PELICULA"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "IMAGEN"), MaxLength(100), Required]
        public string Imagen { get; set; }

        [Column(name: "NOMBRE"), MaxLength(50), Required]
        public string Nombre { get; set; }

        [Column(name: "DESCRIPCION"), MaxLength(500), Required]
        public string Descripcion { get; set; }

        public virtual List<Horario> Horarios { get; set; }

    }

}
