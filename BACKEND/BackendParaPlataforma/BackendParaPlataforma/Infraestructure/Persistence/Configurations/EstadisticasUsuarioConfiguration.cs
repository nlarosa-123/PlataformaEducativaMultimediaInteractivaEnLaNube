using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class EstadisticaUsuarioConfiguration : IEntityTypeConfiguration<EstadisticaUsuario>
    {
        public void Configure(EntityTypeBuilder<EstadisticaUsuario> builder)
        {
            builder.ToTable("estadisticas_usuario");

            builder.HasKey(e => e.IdEstadistica);

            builder.Property(e => e.PorcentajeCoincidenciaIA)
                   .HasPrecision(5, 2); // Ej: 100.00%

            builder.Property(e => e.EmocionMasFrecuente)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(e => e.RachaDiasRegistrados)
                   .IsRequired();

            builder.Property(e => e.UltimaActualizacion);

            builder.HasOne(a => a.Usuario)
                   .WithOne(u => u.EstadisticaUsuario)
                   .HasForeignKey<EstadisticaUsuario>(e => e.IdUsuario);
        }
    }
}