namespace BackendParaPlataforma.dtos
{
    public class DiarioListaDto
    {
        public int Id_Diario { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto_Usuario { get; set; }

        public string NombreEmocion { get; set; }
        public string Emoji { get; set; }
    }
}
