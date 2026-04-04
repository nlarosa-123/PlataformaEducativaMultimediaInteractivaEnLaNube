using BackendParaPlataforma.Dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalisisIAController : ControllerBase
    {
        private readonly IAnalisisIARepository _repository;

        public AnalisisIAController(IAnalisisIARepository repository)
        {
            _repository = repository;
        }

        // ?? GET: api/AnalisisIA
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnalisisIA>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // ?? GET: api/AnalisisIA/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalisisIA>> GetById(int id)
        {
            var analisis = await _repository.GetByIdAsync(id);

            if (analisis == null)
                return NotFound($"No se encontró el análisis con ID {id}");

            return Ok(analisis);
        }

        // ?? GET: api/AnalisisIA/diario/10
        [HttpGet("diario/{diarioId}")]
        public async Task<ActionResult<IEnumerable<AnalisisIADto>>> GetByDiario(int diarioId)
        {
            var result = await _repository.GetByDiarioIdAsync(diarioId);
            return Ok(result);
        }

        // ?? GET: api/AnalisisIA/diario/10/latest
        [HttpGet("diario/{diarioId}/latest")]
        public async Task<ActionResult<AnalisisIA>> GetLatestByDiario(int diarioId)
        {
            var analisis = await _repository.GetLatestByDiarioAsync(diarioId);

            if (analisis == null)
                return NotFound("No hay análisis para este diario");

            return Ok(analisis);
        }

        // ?? POST: api/AnalisisIA
        [HttpPost]
        public async Task<ActionResult<AnalisisIA>> Create([FromBody] AnalisisIA analisis)
        {
            if (analisis == null)
                return BadRequest("Datos inválidos");

            var created = await _repository.CreateAsync(analisis);

            return CreatedAtAction(nameof(GetById), new { id = created.Id_Analisis }, created);
        }

        // ?? PUT: api/AnalisisIA/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnalisisIA analisis)
        {
            if (id != analisis.Id_Analisis)
                return BadRequest("El ID no coincide");

            var updated = await _repository.UpdateAsync(analisis);

            if (!updated)
                return NotFound($"No se encontró el análisis con ID {id}");

            return NoContent();
        }

        // ?? DELETE: api/AnalisisIA/5
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