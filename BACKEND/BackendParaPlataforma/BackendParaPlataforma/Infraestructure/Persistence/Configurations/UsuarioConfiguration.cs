using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendParaPlataforma.Infraestructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");

            // Primary Key
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id_usuario")
                   .ValueGeneratedOnAdd();

            // Nombre
            builder.Property(u => u.Nombre)
                   .HasColumnName("nombre")
                   .IsRequired()
                   .HasMaxLength(150);

            // Email (UNIQUE)
            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            // PasswordHash
            builder.Property(u => u.PasswordHash)
                   .HasColumnName("password_hash")
                   .IsRequired()
                   .HasMaxLength(500);

            // FechaRegistro
            builder.Property(u => u.FechaRegistro)
                   .HasColumnName("fecha_registro")
                   .IsRequired();

            // UltimoLogin
            builder.Property(u => u.UltimoLogin)
                   .HasColumnName("ultimo_login");

            // EstadoActual
            builder.Property(u => u.EstadoActual)
                   .HasColumnName("estado_actual")
                   .HasMaxLength(250);

            // ConfiguracionVoz
            builder.Property(u => u.ConfiguracionVoz)
                   .HasColumnName("configuracion_voz")
                   .HasMaxLength(250);

            // Activo
            builder.Property(u => u.Activo)
                   .HasColumnName("activo")
                   .IsRequired();
        }
    }
}
