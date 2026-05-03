namespace BackendParaPlataforma.Models
{
    public class SentimientoResponse
    {
        public string Sentimiento { get; set; }
        public float Positivo { get; set; }
        public float Negativo { get; set; }
        public float Neutral { get; set; }
        public float Mixto { get; set; }
    }
}