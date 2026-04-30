using OpenAI;
using OpenAI.Chat; 
using BackendParaPlataforma.dtos;

namespace BackendParaPlataforma.OpenAI
{
    public class MetodosOpenAI
    {
        ChatClient client; 

        public MetodosOpenAI (IConfiguration config)
        {
            client = new(model: "gpt-5.4-mini", apiKey: config["OpenAI:Key"]);
 
        }

        public void Test()
        {
            ChatCompletion completion = client.CompleteChat("Say 'this is a test.'");
            Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
        }
    }

}