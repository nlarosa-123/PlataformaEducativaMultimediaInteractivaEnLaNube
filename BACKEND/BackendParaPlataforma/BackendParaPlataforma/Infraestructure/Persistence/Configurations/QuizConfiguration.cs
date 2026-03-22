using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quizzes");

            builder.HasKey(q => q.IdQuiz);

            builder.Property(q => q.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(q => q.Descripcion)
                .HasMaxLength(500);

            // Relación 1 - N con Preguntas
            builder.HasMany(q => q.Preguntas)
                .WithOne(p => p.Quiz)
                .HasForeignKey(p => p.IdQuiz)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}