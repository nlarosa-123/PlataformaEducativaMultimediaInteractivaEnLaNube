namespace BackendParaPlataforma.Entities
{
    public class Emociones
    {
        public int IdEmocion { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Emoji { get; set; } = string.Empty;

        // Valor para estadísticas (-2 a +2)
        public int Valor { get; set; }

        // Relaciones
        //public ICollection<DiarioEmocional> DiariosEmocionales { get; set; } = new List<DiarioEmocional>();
        //public AnalisisIA AnalisisIA { get; set; } = null!;
    }
}