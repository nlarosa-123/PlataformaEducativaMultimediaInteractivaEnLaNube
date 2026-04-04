namespace BackendParaPlataforma.dtos
{
    public class CrearDiarioDto
    {
        public int UsuarioId { get; set; }

        public int EmocionId { get; set; }

        public string Contenido { get; set; } = string.Empty;
    }
}
