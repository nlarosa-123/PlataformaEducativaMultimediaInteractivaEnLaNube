using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Persistence {
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Emociones> Emociones => Set<Emociones>();
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<PreguntaQuiz> PreguntasQuiz { get; set; }
        public DbSet<ProgresoModuloUsuario> ProgresoModuloUsuarios => Set<ProgresoModuloUsuario>();

        public DbSet<DiarioEmocional> DiariosEmocionales => Set<DiarioEmocional>();
        public DbSet<AnalisisIA> AnalisisIA => Set<AnalisisIA>();
        public DbSet<ReflexionMejora> ReflexionesMejora => Set<ReflexionMejora>();
        public DbSet<ProgresoUsuario> ProgresoUsuarios => Set<ProgresoUsuario>();
        //public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<EstadisticaUsuario> EstadisticaUsuario => Set<EstadisticaUsuario>(); 
        public DbSet<Modulos> Modulos => Set<Modulos>(); 
        public DbSet<Lecciones> Lecciones => Set<Lecciones>(); 
        public DbSet<ProgresoLeccionUsuario> ProgresoLeccionUsuario => Set<ProgresoLeccionUsuario>(); 
        public DbSet<OpcionRespuesta> OpcionRespuestas => Set<OpcionRespuesta>(); 
        public DbSet<RespuestaUsuarioQuiz> RespuestaUsuarioQuizzes => Set<RespuestaUsuarioQuiz>(); 


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
