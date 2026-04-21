using BackendParaPlataforma.Azure;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentimentController : ControllerBase
    {
        private readonly MétodosAzure _azure;

        public SentimentController(MétodosAzure azure)
        {
            _azure = azure;
        }
        [HttpPost]
        public async Task<IActionResult> Analyze([FromBody] string text)
        {
            var result = await _azure.Analyze(text);
            return Ok(result);
        }
    }
}
