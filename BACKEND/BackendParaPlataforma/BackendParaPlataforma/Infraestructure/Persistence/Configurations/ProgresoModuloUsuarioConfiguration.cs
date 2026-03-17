using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class ProgresoModuloUsuarioConfiguration : IEntityTypeConfiguration<ProgresoModuloUsuario>
    {
        public void Configure(EntityTypeBuilder<ProgresoModuloUsuario> builder)
        {
            builder.ToTable("ProgresoModuloUsuario");

            builder.HasKey(x => x.Id);

            // índice único: un usuario solo puede tener un progreso por módulo
            builder.HasIndex(x => new { x.IdUsuario, x.IdModulo })
                   .IsUnique();

            builder.Property(x => x.IdUsuario)
                   .IsRequired();

            builder.Property(x => x.IdModulo)
                   .IsRequired();

            builder.Property(x => x.Porcentaje)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(x => x.Completado)
                   .IsRequired();

            builder.Property(x => x.UltimaLeccion);

            #region usuario

            // relación con Usuario
            builder.HasOne(x => x.Usuario)
                   .WithMany(u => u.ProgresosModulos)
                   .HasForeignKey(x => x.IdUsuario)
                   .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}