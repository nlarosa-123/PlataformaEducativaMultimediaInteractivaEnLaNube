using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;


namespace BackendParaPlataforma.Azure
{
    public class MétodosAzure
    {
        static string speechKey = "";
        static string endpoint = "https://italynorth.api.cognitive.microsoft.com/";
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
    }
}
