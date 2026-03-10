using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class EmocionConfiguration : IEntityTypeConfiguration<Emociones>
    {
        public void Configure(EntityTypeBuilder<Emociones> builder)
        {
            builder.ToTable("Emociones");

            builder.HasKey(e => e.IdEmocion);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Emoji)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Valor)
                .IsRequired();
        }
    }
}
