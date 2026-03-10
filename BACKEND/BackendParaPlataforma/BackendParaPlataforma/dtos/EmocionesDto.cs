namespace BackendParaPlataforma.dtos
{
    public class EmocionesDto
    {
        public int IdEmocion { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Emoji { get; set; } = string.Empty;

        public int Valor { get; set; }
    }
}
