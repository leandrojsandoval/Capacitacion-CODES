using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_VENTAS")]
    public class Venta : EntidadBase {

        [Column(name: "ID_VENTAS"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "DETALLE"), MaxLength(200), Required]
        public string Detalle { get; set; }

        [Column(name: "MONTO"), Required]
        public float Monto { get; set; }

    }

}
