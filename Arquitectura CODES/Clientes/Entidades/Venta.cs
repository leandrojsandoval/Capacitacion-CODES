using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_VENTAS")]
    public class Venta : EntidadBase {

        [Column(name: "ID_VENTA"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "DESCRIPCION"), MaxLength(50)]
        public string Descripcion { get; set; }

        [Column(name: "FECHA")]
        public DateTime Fecha { get; set; }

        [Column(name: "CANTIDAD"), Required]
        public int Cantidad {
            get { return Productos.Count; }
        }

        [Column(name: "TOTAL"), Required]
        public double Total {
            get {
                double total = 0;
                foreach (Producto producto in Productos) {
                    total += producto.Precio;
                }
                return total;
            }
        }

        [Column(name: "ID_CLIENTE"), Required]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        public virtual List<Producto> Productos { get; set; }

        public Venta () { Productos = new(); }

    }

}
