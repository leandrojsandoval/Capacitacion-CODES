using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades {

    [Table(name: "TBL_ORDENES")]
    public class Orden : EntidadBase {

        [Column(name: "ID_ORDEN"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "ID_USUARIO"), Required]
        public int IdUsuario { get; set; }

        [Column(name: "ID_HORARIO"), Required]
        public int IdHorario { get; set; }

        [Column(name: "CANTIDAD"), Required]
        public int Cantidad { get; set; }

        [Column(name: "TOTAL"), Required]
        public double Total { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("IdHorario")]
        public virtual Horario Horario { get; set; }

    }
}
