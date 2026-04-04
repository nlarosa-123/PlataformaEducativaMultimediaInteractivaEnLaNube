using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class LeccionesConfiguration : IEntityTypeConfiguration<Lecciones>
    {
        public void Configure(EntityTypeBuilder<Lecciones> builder)
        {
            builder.ToTable("lecciones");

            builder.HasKey(l => l.Id);

            builder.HasOne(l => l.Modulos)
                   .WithMany(m => m.Lecciones)
                   .HasForeignKey(l => l.IdModulo);

            builder.Property(l => l.Titulo)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(l => l.TipoContenido)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(l => l.ContenidoTxt)
                   .HasMaxLength(2000)
                   .IsRequired(false);

            builder.Property(l => l.UrlVideo)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(l => l.UrlAudio)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(l => l.Orden)
                   .IsRequired();

            builder.Property(l => l.Duracion)
                   .IsRequired();

        }
    }
}
