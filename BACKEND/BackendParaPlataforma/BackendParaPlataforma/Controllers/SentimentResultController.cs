using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.FuncionesAux;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentimentResultController : ControllerBase
    {
        private readonly ISentimentResultRepository _repository;
        private readonly IDiarioEmocionalRepository _diarioRepository;
        private readonly MetodosAux _metodosAux;

        public SentimentResultController(ISentimentResultRepository repository,
            IDiarioEmocionalRepository diarioRepository,
            MetodosAux metodosAux)
        {
            _repository = repository;
            _diarioRepository = diarioRepository;
            _metodosAux = metodosAux;
        }

        // 🔹 GET: api/SentimentResult
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SentimentResult>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // 🔹 GET: api/SentimentResult/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SentimentResult>> GetById(int id)
        {
            var sentiment = await _repository.GetByIdAsync(id);

            if (sentiment == null)
                return NotFound($"No se encontró el análisis con ID {id}");

            return Ok(sentiment);
        }

        // 🔹 GET: api/SentimentResult/diario/10
        [HttpGet("diario/{diarioId}")]
        public async Task<ActionResult<IEnumerable<SentimentResult>>> GetByDiario(int diarioId)
        {
            var result = await _repository.GetByDiarioIdAsync(diarioId);

            if (!result.Any())
                return NotFound("No hay análisis para este diario");

            var dto = result.Select(x => new SentimentResultCompleteDto
            {
                Id_Analisis = x.Id_Analisis,
                Id_Diario = x.Id_Diario,
                Provider = x.Provider,
                Sentiment = x.Sentiment,
                Positive = x.Positive.Value,
                Neutral = x.Neutral.Value,
                Negative = x.Negative.Value,
                Coincide_Usuario = x.Coincide_Usuario,
                Confidence = x.Confidence,
                Explanation = x.Explanation
    });

            return Ok(dto);
        }

        // 🔹 GET: api/SentimentResult/diario/10/latest
        [HttpGet("diario/{diarioId}/latest")]
        public async Task<ActionResult<SentimentResult>> GetLatestByDiario(int diarioId)
        {
            var sentiment = await _repository.GetLatestByDiarioAsync(diarioId);

            if (sentiment == null)
                return NotFound("No hay análisis para este diario");

            return Ok(sentiment);
        }

        // 🔹 GET: api/SentimentResult/diario/10/provider/OpenAI
        [HttpGet("diario/{diarioId}/provider/{provider}")]
        public async Task<ActionResult<SentimentResult>> GetByProvider(int diarioId, string provider)
        {
            var result = await _repository.GetByDiarioAndProviderAsync(diarioId, provider);

            if (result == null)
                return NotFound($"No hay análisis para el provider {provider}");

            return Ok(result);
        }

        // 🔹 POST: api/SentimentResult
        [HttpPost]
        public async Task<ActionResult<SentimentResult>> Create([FromBody] SentimentResult sentiment)
        {
            if (sentiment == null)
                return BadRequest("Datos inválidos");

            var created = await _repository.CreateAsync(sentiment);

            return CreatedAtAction(nameof(GetById), new { id = created.Id_Analisis }, created);
        }

        // 🔹 POST: api/SentimentResult/upsert
        [HttpPost("upsert")]
        public async Task<ActionResult<SentimentResult>> Upsert([FromBody] SentimentResult sentiment)
        {
            if (sentiment == null)
                return BadRequest("Datos inválidos");

            var result = await _repository.UpsertAsync(sentiment);

            return Ok(result);
        }

        // 🔹 PUT: api/SentimentResult/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SentimentResult sentiment)
        {
            if (id != sentiment.Id_Analisis)
                return BadRequest("El ID no coincide");

            var updated = await _repository.UpdateAsync(sentiment);

            if (!updated)
                return NotFound($"No se encontró el análisis con ID {id}");

            DiarioEmocional? diarioEmocional = await _diarioRepository.GetByIdAsync(sentiment.Id_Diario);

            await _metodosAux.CrearActualizarEstUsuario(diarioEmocional.Id_Usuario, sentiment.Provider);

            return NoContent();
        }

        // 🔹 DELETE: api/SentimentResult/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound($"No se encontró el análisis con ID {id}");

            return NoContent();
        }
    }
}