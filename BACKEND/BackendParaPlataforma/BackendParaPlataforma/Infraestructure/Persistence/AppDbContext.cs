using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Emociones> Emociones => Set<Emociones>();

        public DbSet<DiarioEmocional> DiariosEmocionales => Set<DiariosEmocionales>();
        public DbSet<AnalisisIA> AnalisisIA => Set<AnalisisIA>();
        public DbSet<ReflexionMejora> ReflexionesMejora => Set<ReflexionMejora>();
        public DbSet<ProgresoUsuario> ProgresoUsuarios => Set<ProgresoUsuario>();
        //public DbSet<Auditoria> Auditorias { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /*public class CreateDiarioDto {
            public int IdEmocionUsuario { get; set; }
            public string TextoUsuario { get; set; }
            public string? AudioUrl { get; set; }
        }

        public DbSet<DiarioEmocional> DiariosEmocionales { get; set; }
        public DbSet<AnalisisIA> AnalisisIA { get; set; }
        public DbSet<ReflexionMejora> ReflexionesMejora { get; set; }
        public DbSet<ProgresoUsuario> ProgresoUsuarios { get; set; }*/
        public DbSet<Auditoria> Auditorias { get; set; }



    }
}
