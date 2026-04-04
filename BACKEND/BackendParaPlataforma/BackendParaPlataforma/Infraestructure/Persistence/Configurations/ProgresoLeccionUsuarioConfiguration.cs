using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class ProgresoLeccionUsuarioConfiguration : IEntityTypeConfiguration<ProgresoLeccionUsuario>
    {
        public void Configure(EntityTypeBuilder<ProgresoLeccionUsuario> builder)
        {
            builder.ToTable("progreso_lecciones_usuario");
             
            builder.HasKey(p => p.Id_Progreso);

            builder.HasOne(p => p.Usuario)
                   .WithMany(u => u.ProgresosLecciones)
                   .HasForeignKey(p => p.Id_Usuario)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Leccion)
                   .WithMany(l => l.ProgresosUsuarios)
                   .HasForeignKey(p => p.Id_Leccion);
             
            builder.Property(p => p.Completado)
                   .IsRequired();

            builder.Property(p => p.Fecha_Completado)
                   .IsRequired(false);

            builder.Property(p => p.Tiempo_Visualizado)
                   .IsRequired()
                   .HasDefaultValue(0);             
        }
    }
}
