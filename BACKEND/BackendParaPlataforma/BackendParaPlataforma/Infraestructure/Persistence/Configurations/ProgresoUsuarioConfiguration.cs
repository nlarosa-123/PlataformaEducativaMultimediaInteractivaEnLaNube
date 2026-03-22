using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendParaPlataforma.Entities;

public class ProgresoUsuarioConfiguration : IEntityTypeConfiguration<ProgresoUsuario> {

    public void Configure(EntityTypeBuilder<ProgresoUsuario> builder) {

        builder.ToTable("progreso_usuario");

        builder.HasKey(p => p.Id_Progreso);

        builder.Property(p => p.Modulo)
               .HasMaxLength(200);
    }
}