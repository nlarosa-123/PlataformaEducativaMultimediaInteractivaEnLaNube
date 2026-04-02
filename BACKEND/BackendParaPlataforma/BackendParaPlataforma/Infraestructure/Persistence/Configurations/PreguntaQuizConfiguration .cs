using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class PreguntaQuizConfiguration : IEntityTypeConfiguration<PreguntaQuiz>
    {
        public void Configure(EntityTypeBuilder<PreguntaQuiz> builder)
        {
            builder.ToTable("preguntas_quiz");

            builder.HasKey(p => p.IdPregunta);

            builder.HasOne(p => p.Quiz)
                   .WithMany(q => q.PreguntaQuizzes)
                   .HasForeignKey(p => p.IdQuiz);

            builder.Property(p => p.Pregunta)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(p => p.Orden)
                   .IsRequired();

        }
    }
}
