using Microsoft.Extensions.Configuration;
using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using System.Threading.Tasks;

namespace BackendParaPlataforma.Services
{
    public class ComprehendService
    {
        private readonly AmazonComprehendClient _client;
        private readonly IConfiguration _config;

        public ComprehendService(IConfiguration config)
        {
            _config = config;

            var accessKey = config["AWS:AccessKey"];
            var secretKey = config["AWS:SecretKey"];
            var region = config["AWS:Region"];

            _client = new AmazonComprehendClient(
                accessKey,
                secretKey,
                RegionEndpoint.GetBySystemName(region)

            );
        }
        
        
        public async Task<DetectSentimentResponse> Analizar(string texto)
        {
            var request = new DetectSentimentRequest
            {
                Text = texto,
                LanguageCode = "es"
            };

            return await _client.DetectSentimentAsync(request);
        }

        /**public async Task<DetectSentimentResponse> Analizar(string texto)
        {
            try
            {
                var request = new DetectSentimentRequest
                {
                    Text = texto,
                    LanguageCode = "es"
                };

                return await _client.DetectSentimentAsync(request);
            }
            catch (AmazonComprehendException ex) when (ex.Message.Contains("subscription"))
            {
                // Simulamos la respuesta de AWS para que puedas seguir trabajando
                return new DetectSentimentResponse
                {
                    Sentiment = SentimentType.MIXED,
                    SentimentScore = new SentimentScore
                    {
                        Mixed = 0.85f,
                        Positive = 0.10f,
                        Negative = 0.05f,
                        Neutral = 0.00f
                    }
                };
        }*/
    }
}
