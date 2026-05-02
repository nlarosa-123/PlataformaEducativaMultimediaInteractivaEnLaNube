using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class SentimentResultConfiguration : IEntityTypeConfiguration<SentimentResult>
    {
        public void Configure(EntityTypeBuilder<SentimentResult> builder)
        {
            builder.ToTable("sentiment_results");

            builder.HasKey(s => s.Id_Analisis);

            builder.Property(s => s.Provider)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.Sentiment)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.Coincide_Usuario);

            builder.Property(s => s.Fecha_Analisis)
                   .IsRequired();

            // Scores estándar
            builder.Property(s => s.Positive)
                   .HasPrecision(5, 4);

            builder.Property(s => s.Neutral)
                   .HasPrecision(5, 4);

            builder.Property(s => s.Negative)
                   .HasPrecision(5, 4);

            // Google-style
            builder.Property(s => s.Score)
                   .HasPrecision(5, 4);

            builder.Property(s => s.Magnitude)
                   .HasPrecision(5, 4);

            // OpenAI-style
            builder.Property(s => s.Confidence)
                   .HasPrecision(5, 4);

            builder.Property(s => s.Explanation)
                   .HasMaxLength(500);

            builder.Property(s => s.RawJson)
                   .HasColumnType("nvarchar(max)");

            // Relación 1:1 con DiarioEmocional
            builder.HasOne(s => s.DiarioEmocional)
                   .WithMany(d => d.SentimentResults)
                   .HasForeignKey(s => s.Id_Diario)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
