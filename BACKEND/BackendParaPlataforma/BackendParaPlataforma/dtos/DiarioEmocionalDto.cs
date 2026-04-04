namespace BackendParaPlataforma.dtos
{
    public class DiarioEmocionalDto
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int EmocionId { get; set; }

        public string Contenido { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public EmocionesDto? Emocion { get; set; }
    }
}
