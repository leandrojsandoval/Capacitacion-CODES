using Framework.Common;
using Microsoft.EntityFrameworkCore;
using ARQ.Entidades;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ARQ.Datos.EFScafolding {
    public class ARQContext : DbContext {
        public ARQContext (DbContextOptions<ARQContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Funcionalidad> Funcionalidades { get; set; }
        public DbSet<FuncionalidadRol> FuncionalidadRoles { get; set; }
        public DbSet<TipoAcceso> TiposAccesos { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {

            this.SetKeys(modelBuilder);
            this.RemoveCascadeDeleteForAll(modelBuilder);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Usuario)        // Una orden tiene un usuario
                .WithMany(u => u.Ordenes)      // Un usuario puede tener muchas ordenes
                .HasForeignKey(o => o.IdUsuario);  // La clave foránea en Orden es IdUsuario



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
