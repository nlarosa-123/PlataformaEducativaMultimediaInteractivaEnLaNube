using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Azure;
using BackendParaPlataforma.dtos;
using Microsoft.Extensions.Configuration;
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


        public async Task<SentimentResultDto> Analizar(string texto)
        {
            var request = new DetectSentimentRequest
            {
                Text = texto,
                LanguageCode = "es"
            };

            var resultadoAWS = await _client.DetectSentimentAsync(request);

            return new SentimentResultDto
            {
                Sentiment = resultadoAWS.Sentiment.ToString(),
                Positive = (double)resultadoAWS.SentimentScore.Positive,
                Neutral = (double)resultadoAWS.SentimentScore.Neutral,
                Negative = (double)resultadoAWS.SentimentScore.Negative
            };
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
