using Azure;
using Azure.AI.TextAnalytics;
using BackendParaPlataforma.dtos;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace BackendParaPlataforma.Azure
{
    public class MétodosAzure
    {
        static string speechKey = "";
        static string endpoint = "https://italynorth.api.cognitive.microsoft.com/";

        TextAnalyticsClient clientSA;
        public MétodosAzure(IConfiguration config)
        {
            var endpointSA = config["AzureAI:Endpoint"];
            var keySA = config["AzureAI:Key"];
            clientSA = new TextAnalyticsClient(
                new Uri(endpointSA),
                new AzureKeyCredential(keySA)
            );
        }
        public async Task<string> ConvertSpeechToText(Stream audioStream)
        {
            try
            {
                var speechConfig = SpeechConfig.FromEndpoint(new Uri(endpoint), speechKey);
                speechConfig.SpeechRecognitionLanguage = "es-ES";

                // FORMATO DEL AUDIO (clave)
                var format = AudioStreamFormat.GetWaveFormatPCM(16000, 16, 1);

                var pushStream = AudioInputStream.CreatePushStream(format);

                // copiar el stream al pushStream
                byte[] buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = await audioStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    pushStream.Write(buffer, bytesRead);
                }

                pushStream.Close();

                var audioConfig = AudioConfig.FromStreamInput(pushStream);

                using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

                var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();

                return speechRecognitionResult.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR CONFIG: " + ex.Message);
                Console.WriteLine("ERROR CONFIG: " + ex);
                throw;
            }

        }

        public async Task<SentimentResultDto> Analyze(string text)
        {
            var response = clientSA.AnalyzeSentiment(text);
            return new SentimentResultDto
            {
                Sentiment = response.Value.Sentiment.ToString(),
                Positive = response.Value.ConfidenceScores.Positive,
                Neutral = response.Value.ConfidenceScores.Neutral,
                Negative = response.Value.ConfidenceScores.Negative
            };
        }
    }
}
