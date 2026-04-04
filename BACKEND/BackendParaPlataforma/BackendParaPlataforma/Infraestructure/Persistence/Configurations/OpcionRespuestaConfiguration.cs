using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class OpcionRespuestaConfiguration : IEntityTypeConfiguration<OpcionRespuesta>
    {
        public void Configure(EntityTypeBuilder<OpcionRespuesta> builder)
        {
            builder.ToTable("opciones_respuesta");

            builder.HasKey(o => o.IdOpcion);

            builder.HasOne(o => o.Pregunta)
                   .WithMany(p => p.Opciones)
                   .HasForeignKey(o => o.IdPregunta);

            builder.Property(o => o.TextoOpcion)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(o => o.EsCorrecta)
                   .IsRequired();
        }
    }
}
