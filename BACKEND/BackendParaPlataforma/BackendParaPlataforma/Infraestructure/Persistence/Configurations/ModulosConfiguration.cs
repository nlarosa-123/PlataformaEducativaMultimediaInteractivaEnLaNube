using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class ModulosConfiguration : IEntityTypeConfiguration<Modulos>
    {
        public void Configure(EntityTypeBuilder<Modulos> builder)
        {
            builder.ToTable("modulos");

            builder.HasKey(m => m.IdModulo);

            builder.Property(m => m.Titulo)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(m => m.Descripcion)
                   .IsRequired()
                   .HasMaxLength(1000);
        }
    }
}
