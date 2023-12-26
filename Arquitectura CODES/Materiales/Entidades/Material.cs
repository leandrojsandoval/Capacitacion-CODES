using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_MATERIALES")]
    public class Material : EntidadBase {

        [Column(name: "ID_MATERIAL_RODILLO"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "NOMBRE"), MaxLength(50), Required]
        public string Nombre { get; set; }

        [Column(name: "DESCRIPCION"), MaxLength(200)]
        public string Descripcion { get; set; }

        [Column(name: "DUREZA")]
        public int Dureza { get; set; }

        [Column(name: "MULTIPLICADOR_TONELADAS"), Required]
        public double MultiplicadorToneladas { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [Column(name: "ID_PRODUCTO"), Required]
        public int IdProducto { get; set; }

    }

}
