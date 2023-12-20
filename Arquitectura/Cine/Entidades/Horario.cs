using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_HORARIOS")]
    public class Horario : EntidadBase {

        [Column(name: "ID_HORARIO"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "ID_PELICULA"), Required]
        public int IdPelicula { get; set; }

        [Column(name: "ID_SUCURSAL"), Required]
        public int IdSucursal { get; set; }

        [Column(name: "HORA"), MaxLength(10), Required]
        public string Hora { get; set; }

        [ForeignKey("IdPelicula")]
        public virtual Pelicula Pelicula { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

    }

}
