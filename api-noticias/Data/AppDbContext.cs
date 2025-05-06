using Microsoft.EntityFrameworkCore;
using api_noticias.Models;

namespace api_noticias.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {}

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Clasificaciones> Clasificaciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Noticias>()
                .HasOne(n => n.Categoria)
                .WithMany(c => c.Noticias)
                .HasForeignKey(n => n.idCategoria);

            modelBuilder.Entity<Noticias>()
                .HasOne(n => n.Clasificacion)
                .WithMany(cl => cl.Noticias)
                .HasForeignKey(n => n.idClasificacion);

            base.OnModelCreating(modelBuilder);
        }
    }
}
