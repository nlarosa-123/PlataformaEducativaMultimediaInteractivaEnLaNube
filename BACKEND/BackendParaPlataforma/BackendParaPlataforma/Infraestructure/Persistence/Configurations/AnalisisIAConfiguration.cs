using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendParaPlataforma.Entities;

public class AnalisisIAConfiguration : IEntityTypeConfiguration<AnalisisIA> {

    public void Configure(EntityTypeBuilder<AnalisisIA> builder) {

        builder.ToTable("analisis_ia");

        builder.HasKey(a => a.Id_Analisis);

        builder.Property(a => a.Tono_Detectado)
               .HasMaxLength(50);

        builder.Property(a => a.Confianza)
               .HasPrecision(3, 2);
    }
}