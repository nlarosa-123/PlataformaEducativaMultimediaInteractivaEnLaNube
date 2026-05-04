using BackendParaPlataforma.Google;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentimentGoogleController : ControllerBase
    {
        private readonly MétodosGoogle _google;

        public SentimentGoogleController(MétodosGoogle google)
        {
            _google = google;
        }

        [HttpPost]
        public async Task<IActionResult> Analyze([FromBody] string text)
        {
            var result = await _google.Analyze(text);
            return Ok(result);
        }
    }
}