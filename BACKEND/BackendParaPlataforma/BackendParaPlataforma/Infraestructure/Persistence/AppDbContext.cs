using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Emociones> Emociones => Set<Emociones>();
        public DbSet<EstadisticaUsuario> Stats => Set<EstadisticaUsuario>();
        public DbSet<Modulos> Modulos => Set<Modulos>();
        public DbSet<ProgresoLeccionesUsuario> ProgresoLecciones => Set<ProgresoLeccionesUsuario>();
        public DbSet<Lecciones> Lecciones => Set<Lecciones>(); 
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
