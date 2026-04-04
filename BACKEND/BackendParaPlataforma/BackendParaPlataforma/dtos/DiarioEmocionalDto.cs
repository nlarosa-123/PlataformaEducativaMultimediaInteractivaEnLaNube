namespace BackendParaPlataforma.dtos
{
    public class DiarioEmocionalDto
    {
        public int Id_Diario { get; set; }
        public int Id_Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto_Usuario { get; set; }

        public string NombreEmocion { get; set; }
        public string Emoji { get; set; }

        public string? Tono { get; set; }
        public string? Reflexion { get; set; }
    }
}
