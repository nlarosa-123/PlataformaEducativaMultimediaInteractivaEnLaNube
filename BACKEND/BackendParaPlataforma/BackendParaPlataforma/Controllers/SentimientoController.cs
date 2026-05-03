using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Models;
using BackendParaPlataforma.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/sentimiento")]
    public class SentimientoController : ControllerBase
    {
        private readonly ComprehendService _service;
        private readonly AppDbContext _context; // Sustituye por el nombre de tu DbContext real

        // Inyectamos el servicio y el contexto de BD
        public SentimientoController(ComprehendService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost("analizar")]
        public async Task<IActionResult> Analizar([FromBody] SentimientoRequest request)
        {
            // Llamada al servicio de AWS
            var result = await _service.Analizar(request.Texto);

            // Mapeo a la respuesta. Usamos .Value para obtener el string del sentimiento
            var response = new SentimientoResponse
            {
                Sentimiento = result.Sentiment.Value,
                Positivo = (float)result.SentimentScore.Positive,
                Negativo = (float)result.SentimentScore.Negative,
                Neutral = (float)result.SentimentScore.Neutral,
                Mixto = (float)result.SentimentScore.Mixed
            };

            return Ok(response);
        }

        [HttpPost("guardar")]
        public async Task<IActionResult> Guardar([FromBody] RegistroSentimiento data)
        {
            // Asignamos fecha actual si no viene del frontend
            data.Fecha = DateTime.Now;

            // Guardado real en SQL Server
            _context.Sentimientos.Add(data);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Guardado con éxito", id = data.Id });
        }
    }
}


