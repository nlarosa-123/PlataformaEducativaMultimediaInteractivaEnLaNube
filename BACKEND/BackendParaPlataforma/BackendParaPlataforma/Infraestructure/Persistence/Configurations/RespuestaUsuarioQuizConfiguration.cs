using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class RespuestaUsuarioQuizConfiguration : IEntityTypeConfiguration<RespuestaUsuarioQuiz>
    {
        public void Configure(EntityTypeBuilder<RespuestaUsuarioQuiz> builder)
        {
            builder.ToTable("respuestas_usuario_quiz");

            builder.HasKey(r => r.IdRespuesta);

            builder.HasOne(r => r.Usuario)
                   .WithMany(u => u.RespuestaUsuarioQuizzes)
                   .HasForeignKey(r => r.IdUsuario);

            builder.HasOne(r => r.Pregunta)
                   .WithMany(p => p.respuestaUsuarioQuizzes)
                   .HasForeignKey(r => r.IdPregunta)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.OpcionRespuesta)
                   .WithMany(o => o.respuestaUsuarioQuizzes)
                   .HasForeignKey(r => r.IdOpcionElegida)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Correcta)
                   .IsRequired();

            builder.Property(r => r.FechaRespuesta)
                   .IsRequired();

        }
    }
}
