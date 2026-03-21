using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Emociones> Emociones => Set<Emociones>();
        public DbSet<Estadisticas> Estadisticas => Set<Estadisticas>(); 


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
