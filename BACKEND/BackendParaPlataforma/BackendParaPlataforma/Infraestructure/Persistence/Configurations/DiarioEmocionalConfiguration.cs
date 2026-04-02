using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendParaPlataforma.Entities;

public class DiarioEmocionalConfiguration : IEntityTypeConfiguration<DiarioEmocional> {

    public void Configure(EntityTypeBuilder<DiarioEmocional> builder) {

        builder.ToTable("diarios_emocionales");

        builder.HasKey(d => d.Id_Diario);

        builder.Property(d => d.Texto_Usuario)
               .HasMaxLength(1000);

        builder.Property(d => d.Audio_Url)
               .IsRequired(false);

        builder.Property(d => d.Fecha_Creacion)
               .HasDefaultValueSql("GETDATE()");

        builder.HasOne(d => d.Usuario)
       .WithMany(u => u.Diarios)
       .HasForeignKey(d => d.Id_Usuario);

        builder.HasOne(d => d.Emocion)
       .WithMany(u => u.DiariosEmocionales)
       .HasForeignKey(d => d.Id_Emocion_Usuario);
    }
}