using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendParaPlataforma.Entities;

public class ReflexionMejoraConfiguration : IEntityTypeConfiguration<ReflexionMejora> {

    public void Configure(EntityTypeBuilder<ReflexionMejora> builder) {

        builder.ToTable("reflexiones_mejora");

        builder.HasKey(r => r.Id_Reflexion);

        builder.Property(r => r.Texto_Reflexion)
               .HasMaxLength(1000);

        builder.Property(r => r.Fecha_Creacion);

        builder.HasOne(r => r.DiarioEmocional)
            .WithOne(r => r.ReflexionMejora)
            .HasForeignKey<ReflexionMejora>(r => r.Id_Diario);
    }
}