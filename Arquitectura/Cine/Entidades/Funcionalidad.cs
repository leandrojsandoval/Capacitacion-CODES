using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARQ.Entidades
{
    [Table(name: "TBL_FUNCIONALIDADES")]
    public class Funcionalidad : EntidadBase
    {
        [Column(name: "ID_FUNCIONALIDAD"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "DESCRIPCION"), MaxLength(100)]
        public string Descripcion { get; set; }

    }
}
