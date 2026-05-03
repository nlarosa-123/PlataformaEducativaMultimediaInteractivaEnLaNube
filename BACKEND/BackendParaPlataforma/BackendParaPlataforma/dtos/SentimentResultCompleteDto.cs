namespace BackendParaPlataforma.dtos
{
    public class SentimentResultCompleteDto
    {
        public int Id_Analisis { get; set; }
        public int Id_Diario { get; set; }
        public string Provider { get; set; }
        public string Sentiment { get; set; }
        public double Positive { get; set; }
        public double Neutral { get; set; }
        public double Negative { get; set; }
        public bool? Coincide_Usuario { get; set; }
    }
}
