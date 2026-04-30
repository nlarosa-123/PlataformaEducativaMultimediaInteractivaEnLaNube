using OpenAI;
using OpenAI.Chat;
using BackendParaPlataforma.dtos;
using System.Text.Json.Schema;
using System.Text.Json.Nodes;
using System.Text.Json;
using System;
namespace BackendParaPlataforma.OpenAI
{
    public class MetodosOpenAI
    {
        ChatClient client;

        public MetodosOpenAI(IConfiguration config)
        {
            client = new(model: "gpt-5.4-mini", apiKey: config["OpenAI:Key"]);

        }

        public async Task<SentimentResultDto> Analyze(string texto)
        {
            //Generar el 'schema' de SentimentResultDto para la salida estructurada de la peticion OpenAI

            var exporterOptions = new JsonSchemaExporterOptions
            {
                // Esto marca el tipo raíz como no nulo
                TreatNullObliviousAsNonNullable = true,
                TransformSchemaNode = (context, schema) =>
                {
                    // Si el esquema que se está procesando es un objeto, añadimos la restricción
                    if (schema is JsonObject jsonSchemaObj && jsonSchemaObj.ContainsKey("properties"))
                    {
                        jsonSchemaObj["additionalProperties"] = false;
                    }

                    //Sentiment solo puede valer "Negative", "Neutral" o "Positive"
                    if (schema is JsonObject jsonSchemaObj2 && context.PropertyInfo?.Name == "Sentiment")
                    {
                        jsonSchemaObj2["enum"] = new JsonArray { "Negative", "Neutral", "Positive" };
                    }

                    return schema;
                }
            };

            JsonNode schema = JsonSerializerOptions.Default.GetJsonSchemaAsNode(typeof(SentimentResultDto), exporterOptions);

            //User prompt
            ChatMessage peticion = new UserChatMessage(String.Format("Analiza las emociones del texto {0}", texto));

            //System Prompt
            ChatMessage contexto = new SystemChatMessage("Eres un psicologo profesional experto en analizar las emociones de las palabras del paciente");

            //Salida estructurada 
            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                                jsonSchemaFormatName: "sentiment_analyze",
                                jsonSchema: BinaryData.FromString(schema.ToString()),
                                jsonSchemaIsStrict: true)

            };

            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add(contexto);
            messages.Add(peticion);

            //Llamada a la api de OpenAI
            ChatCompletion completion = await client.CompleteChatAsync(messages, options);
            Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");

            var answer = JsonSerializer.Deserialize<SentimentResultDto>(completion.Content[0].Text);
            if (answer != null)
            {
                return answer;
            }
            return new SentimentResultDto();
        }
    }

}