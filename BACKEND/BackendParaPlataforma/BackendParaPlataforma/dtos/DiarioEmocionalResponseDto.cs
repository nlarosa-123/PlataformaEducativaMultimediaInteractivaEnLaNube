namespace BackendParaPlataforma.dtos
{
    public class DiarioEmocionalResponseDto
    {
        public int Id_Diario { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Emocion_Usuario { get; set; }
        public string Texto_Usuario { get; set; }
        public string Audio_Url { get; set; }
        public DateTime Fecha { get; set; }
    }
}
