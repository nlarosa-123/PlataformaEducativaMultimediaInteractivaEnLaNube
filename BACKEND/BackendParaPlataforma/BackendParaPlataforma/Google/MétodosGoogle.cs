using System.Text;
using System.Text.Json;
using BackendParaPlataforma.dtos;

namespace BackendParaPlataforma.Google
{
    public class MétodosGoogle
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public MétodosGoogle(IConfiguration config)
        {
            _apiKey = config["GoogleAI:Key"] ?? throw new ArgumentNullException("GoogleAI:Key no configurada");
            _httpClient = new HttpClient();
        }

        public async Task<SentimentResultDto> Analyze(string text)
        {
            var url = $"https://language.googleapis.com/v1/documents:analyzeSentiment?key={_apiKey}";

            var body = new
            {
                document = new
                {
                    type = "PLAIN_TEXT",
                    language = "es",
                    content = text
                },
                encodingType = "UTF8"
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new SentimentResultDto
                {
                    Sentiment = "Error",
                    Positive = 0,
                    Neutral = 0,
                    Negative = 0,
                    Confidence = 0,
                    Explanation = $"Error Google API: {responseBody}"
                };
            }

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            var score = root
                .GetProperty("documentSentiment")
                .GetProperty("score")
                .GetDouble();

            var magnitude = root
                .GetProperty("documentSentiment")
                .GetProperty("magnitude")
                .GetDouble();

            string sentiment;
            double positive = 0, neutral = 0, negative = 0;

            if (Math.Abs(score) < 0.25 && magnitude > 0.5)
            {
                sentiment = "Mixed";
                positive = 0.25;
                negative = 0.25;
                neutral = 0.5;
            }
            else if (score >= 0.25)
            {
                sentiment = "Positive";
                positive = score;
            }
            else if (score <= -0.25)
            {
                sentiment = "Negative";
                negative = Math.Abs(score);
            }
            else
            {
                sentiment = "Neutral";
                neutral = 1 - Math.Abs(score);
            }

            return new SentimentResultDto
            {
                Sentiment = sentiment,
                Positive = positive,
                Neutral = neutral,
                Negative = negative,
                Magnitude = magnitude,
                Score = score
            };
        }
    }
}