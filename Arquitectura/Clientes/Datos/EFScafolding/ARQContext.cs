using Framework.Common;
using Microsoft.EntityFrameworkCore;
using ARQ.Entidades;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ARQ.Datos.EFScafolding {

    public class ARQContext : DbContext {

        public ARQContext (DbContextOptions<ARQContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Funcionalidad> Funcionalidades { get; set; }
        public DbSet<FuncionalidadRol> FuncionalidadRoles { get; set; }
        public DbSet<TipoAcceso> TiposAccesos { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            this.SetKeys(modelBuilder);
            this.RemoveCascadeDeleteForAll(modelBuilder);
        }

        private void SetKeys (ModelBuilder modelBuilder) {
            modelBuilder.Entity<FuncionalidadRol>(entity => {
                entity.HasKey(e => new { e.IdFuncionalidad, e.IdRol });
            });
        }

        private void RemoveCascadeDeleteForAll (ModelBuilder builder) {
            var cascadeFKs = builder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetForeignKeys())
                            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs) {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

    }

}
