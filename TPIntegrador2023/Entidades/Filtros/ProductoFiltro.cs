namespace Entidades.Filtros
{

    /* LS: Agrego tanto Precio como Categoria para realizar un filtro 
     * por estos campos (PUNTO 4) */

    public class ProductoFiltro
    {
        public virtual int? Id { get; set; }
        public virtual string Descripcion { get; set; }

        public virtual int? Precio { get; set; }
        public virtual int? IdCategoria { get; set; }
    }
}
