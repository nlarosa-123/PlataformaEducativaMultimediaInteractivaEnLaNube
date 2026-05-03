using System.Text.Json.Serialization;

namespace BackendParaPlataforma.dtos
{
    public class SentimentResultDto
    {
        [JsonRequired]
        public string Sentiment { get; set; }

        [JsonRequired]
        public double Positive { get; set; }

        [JsonRequired]
        public double Neutral { get; set; }

        [JsonRequired]
        public double Negative { get; set; }

        [JsonRequired]
        public double? Confidence { get; set; }

        [JsonRequired]
        public String? Explanation { get; set; }
    }
}
