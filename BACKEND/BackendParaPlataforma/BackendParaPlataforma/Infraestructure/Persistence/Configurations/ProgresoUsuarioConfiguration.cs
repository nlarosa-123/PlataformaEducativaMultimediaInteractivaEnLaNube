using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendParaPlataforma.Entities;

public class ProgresoUsuarioConfiguration : IEntityTypeConfiguration<ProgresoUsuario> {

    public void Configure(EntityTypeBuilder<ProgresoUsuario> builder) {

        builder.ToTable("progreso_usuario");

        builder.HasKey(p => p.Id_Progreso);

        builder.Property(p => p.Modulo)
               .HasMaxLength(200);
        builder.Property(p => p.Porcentaje);
        builder.Property(p => p.Ultima_Actualizacion);
        builder.HasOne(p => p.Usuario)
            .WithMany(p => p.ProgresosUsuarios)
            .HasForeignKey(p => p.Id_Usuario);
    }
}