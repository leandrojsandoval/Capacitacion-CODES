using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_SUCURSALES")]
    public class Sucursal : EntidadBase {

        [Column(name: "ID_SUCURSAL"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "NOMBRE"), MaxLength(50), Required]
        public string Nombre { get; set; }

        [Column(name: "PRECIO"), Required]
        public double Precio { get; set; }

        public virtual List<Horario> Horarios { get; set; }

    }

}
