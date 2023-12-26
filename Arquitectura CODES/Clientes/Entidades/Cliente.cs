using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_CLIENTES")]
    public class Cliente : EntidadBase {

        [Column(name: "ID_CLIENTE"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "NOMBRE"), MaxLength(50), Required]
        public string Nombre { get; set; }

        [Column(name: "APELLIDO"), MaxLength(50), Required]
        public string Apellido { get; set; }

        [Column(name: "FECHA_NACIMIENTO")]
        public DateTime FechaNacimiento { get; set; }

        public virtual List<Venta> Ventas { get; set; }

    }

}
