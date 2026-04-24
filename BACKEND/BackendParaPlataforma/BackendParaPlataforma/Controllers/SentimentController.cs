using BackendParaPlataforma.Azure;
using BackendParaPlataforma.OpenAI; 
using BackendParaPlataforma.FuncionesAux;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentimentController : ControllerBase
    {
        private readonly MétodosAzure _azure;
        private readonly MetodosOpenAI _openAI; 

        public SentimentController(MétodosAzure azure, MetodosOpenAI openAI)
        {
            _azure = azure;
            _openAI = openAI; 
        }
        [HttpPost]
        public async Task<IActionResult> Analyze([FromBody] string text)
        {
            var result = await _azure.Analyze(text);

            //Probando si OpenAI funciona
            _openAI.Test(); 

            return Ok(result);
        }
    }
}
