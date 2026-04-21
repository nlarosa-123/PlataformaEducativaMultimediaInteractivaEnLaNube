using BackendParaPlataforma.Azure;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeechController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SpeechToText(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Archivo inválido");

                using var stream = file.OpenReadStream();

                var azure = new BackendParaPlataforma.Azure.MétodosAzure(null);

                var text = await azure.ConvertSpeechToText(stream);

                return Ok(new { text });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
