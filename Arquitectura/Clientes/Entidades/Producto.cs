using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_PRODUCTOS")]
    public class Producto : EntidadBase {

        [Column(name: "ID_PRODUCTO"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "NOMBRE"), MaxLength(50), Required]
        public string Nombre { get; set; }

        [Column(name: "DESCRIPCION"), MaxLength(200)]
        public string Descripcion { get; set; }

        [Column(name: "PRECIO"), Required]
        public double Precio { get; set; }

        [Column(name: "ID_VENTA")]
        public int? IdVenta { get; set; }

        [ForeignKey("IdVenta")]
        public virtual Venta Venta { get; set; }

    }

}
