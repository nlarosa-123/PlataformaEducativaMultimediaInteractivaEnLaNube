using BackendParaPlataforma.Azure;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.FuncionesAux;
using BackendParaPlataforma.Infraestructure.Repositories;
using BackendParaPlataforma.OpenAI;
using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Services;

namespace BackendParaPlataforma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiarioEmocionalController : ControllerBase
    {
        private readonly IDiarioEmocionalRepository _repository;
        private readonly ISentimentResultRepository _sentimentResultrepository;
        private readonly MetodosAux _metodosAux;
        private readonly MétodosAzure _azureService;
        private readonly MetodosOpenAI _openAIService;
        private readonly ComprehendService _comprehendService;
        public DiarioEmocionalController(IDiarioEmocionalRepository repository,
            ISentimentResultRepository sentimentResultrepository,
            MetodosAux metodosAux,
            MétodosAzure azureService,
            MetodosOpenAI openAIService,
            ComprehendService comprehendService)
        {
            _repository = repository;
            _metodosAux = metodosAux;
            _sentimentResultrepository = sentimentResultrepository;
            _azureService = azureService;
            _openAIService = openAIService;
            _comprehendService = comprehendService;
        }

        // 📌 GET: api/DiarioEmocional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiarioEmocional>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // 📌 GET: api/DiarioEmocional/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiarioEmocional>> GetById(int id)
        {
            var diario = await _repository.GetByIdAsync(id);

            if (diario == null)
                return NotFound($"No se encontró el diario con ID {id}");

            return Ok(diario);
        }

        // 📊 GET: api/DiarioEmocional/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<DiarioListaDto>>> GetByUsuario(int usuarioId)
        {
            var diarios = await _repository.GetByUsuarioAsync(usuarioId);
            return Ok(diarios);
        }

        // 📅 GET: api/DiarioEmocional/usuario/1/fecha/2026-04-03
        [HttpGet("usuario/{usuarioId}/fecha")]
        public async Task<ActionResult<List<DiarioListaDto>>> GetByFecha(
    int usuarioId,
    [FromQuery] DateTime fecha)
        {
            var result = await _repository.GetByUsuarioYFechaAsync(usuarioId, fecha);
            return Ok(result);
        }

        // 🚀 GET: api/DiarioEmocional/usuario/1/latest
        [HttpGet("usuario/{usuarioId}/latest")]
        public async Task<ActionResult<DiarioEmocionalDto>> GetLatest(int usuarioId)
        {
            var diario = await _repository.GetLatestByUsuarioAsync(usuarioId);

            if (diario == null)
                return NotFound();

            return Ok(diario);
        }

        // 📌 POST: api/DiarioEmocional
        [HttpPost]
        public async Task<ActionResult<DiarioEmocional>> Create([FromBody] DiarioEmocional diario)
        {
            if (diario == null)
                return BadRequest("Datos inválidos");

            // ✅ 1. Guardar diario primero
            var created = await _repository.CreateAsync(diario);

            #region resultado de análisis de diferentes IAs

            #region Azure

            // ✅ 2. Analizar con Azure
            var resultadoAzure = await _azureService.Analyze(created.Texto_Usuario);

            // ✅ 3. Guardar resultado
            var sentimentResult = new SentimentResult()
            {
                Id_Diario = created.Id_Diario,
                Fecha_Analisis = DateTime.UtcNow,
                Provider = "Azure",
                Sentiment = resultadoAzure.Sentiment,
                Positive = resultadoAzure.Positive,
                Neutral = resultadoAzure.Neutral,
                Negative = resultadoAzure.Negative
            };

            await _sentimentResultrepository.UpsertAsync(sentimentResult);

            #endregion Azure

            #region OpenAI

            // Analizar con OpenAI 
            var resultadoOpenAI = await _openAIService.Analyze(created.Texto_Usuario);

            sentimentResult = new SentimentResult()
            {
                Id_Diario = created.Id_Diario,
                Fecha_Analisis = DateTime.UtcNow,
                Provider = "OpenAI",
                Sentiment = resultadoAzure.Sentiment,
                Positive = resultadoAzure.Positive,
                Neutral = resultadoAzure.Neutral,
                Negative = resultadoAzure.Negative,
                Confidence = resultadoOpenAI.Confidence,
                Explanation = resultadoOpenAI.Explanation
            };

            await _sentimentResultrepository.UpsertAsync(sentimentResult);

            #endregion OpenAI

            #region AWS

            // Analizar con AWS 
            var resultadoAWS = await _comprehendService.Analizar(created.Texto_Usuario);

            sentimentResult = new SentimentResult()
            {
                Id_Diario = created.Id_Diario,
                Fecha_Analisis = DateTime.UtcNow,
                Provider = "AWS",
                Sentiment = resultadoAWS.Sentiment,
                Positive = resultadoAWS.Positive,
                Neutral = resultadoAWS.Neutral,
                Negative = resultadoAWS.Negative
            };

            await _sentimentResultrepository.UpsertAsync(sentimentResult);

            #endregion AWS

            #endregion resultado de análisis de diferentes IAs

            // ✅ 4. Actualizar estadísticas
            await _metodosAux.CrearActualizarEstUsuario(created.Id_Usuario);

            return CreatedAtAction(nameof(GetById), new { id = created.Id_Diario }, new DiarioEmocionalResponseDto
            {
                Id_Diario = created.Id_Diario,
                Id_Usuario = created.Id_Usuario,
                Texto_Usuario = created.Texto_Usuario,
                Fecha = created.Fecha,
                Id_Emocion_Usuario = created.Id_Emocion_Usuario,
                Audio_Url = created.Audio_Url
            });
        }

        // 📌 PUT: api/DiarioEmocional/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DiarioEmocional diario)
        {
            if (id != diario.Id_Diario)
                return BadRequest("El ID no coincide");

            var updated = await _repository.UpdateAsync(diario);

            if (!updated)
                return NotFound($"No se encontró el diario con ID {id}");

            await _metodosAux.CrearActualizarEstUsuario(diario.Id_Usuario); 
            
            return NoContent();
        }

        // 📌 DELETE: api/DiarioEmocional/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var diario = await _repository.GetByIdAsync(id); 
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound($"No se encontró el diario con ID {id}");

            if (diario != null)
            {
                await _metodosAux.CrearActualizarEstUsuario(diario.Id_Usuario);
            }

            return NoContent();
        }
        [HttpGet("usuario/{usuarioId}/hoy")]
        public async Task<ActionResult<DiarioEmocionalDto>> GetHoy(int usuarioId)
        {
            var diario = await _repository.GetHoyByUsuarioAsync(usuarioId);

            if (diario == null)
                return NotFound("No hay registro hoy");

            return Ok(diario);
        }
        
    }
}