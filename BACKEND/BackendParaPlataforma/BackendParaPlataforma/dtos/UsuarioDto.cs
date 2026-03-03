namespace BackendParaPlataforma.dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public DateTime? UltimoLogin { get; set; }

        public string? EstadoActual { get; set; }

        public string? ConfiguracionVoz { get; set; }

        public bool Activo { get; set; }
    }
}
