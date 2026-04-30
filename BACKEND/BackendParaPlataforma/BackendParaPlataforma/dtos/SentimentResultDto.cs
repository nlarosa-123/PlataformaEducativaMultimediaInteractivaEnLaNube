namespace BackendParaPlataforma.dtos
{
    public class SentimentResultDto
    {
        public string Sentiment { get; set; }
        public double Positive { get; set; }
        public double Neutral { get; set; }
        public double Negative { get; set; }
    }
}
