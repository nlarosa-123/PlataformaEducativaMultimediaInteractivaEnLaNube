using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmocionesController : ControllerBase
    {
        private readonly IEmocionesRepository _repository;

        public EmocionesController(IEmocionesRepository repository)
        {
            _repository = repository;
        }

        // 📌 GET: api/Emociones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emociones>>> GetAll()
        {
            var emociones = await _repository.GetAllAsync();
            return Ok(emociones);
        }

        // 📌 GET: api/Emociones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emociones>> GetById(int id)
        {
            var emocion = await _repository.GetByIdAsync(id);

            if (emocion == null)
                return NotFound($"No se encontró la emoción con ID {id}");

            return Ok(emocion);
        }

        // 🔍 GET: api/Emociones/nombre/feliz
        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<Emociones>> GetByNombre(string nombre)
        {
            var emocion = await _repository.GetByNombreAsync(nombre);

            if (emocion == null)
                return NotFound($"No se encontró la emoción '{nombre}'");

            return Ok(emocion);
        }

        // 🔥 GET: api/Emociones/rango?min=-1&max=2
        [HttpGet("rango")]
        public async Task<ActionResult<IEnumerable<Emociones>>> GetByRango([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var emociones = await _repository.GetByRangoValorAsync(min, max);
            return Ok(emociones);
        }

        // 📌 POST: api/Emociones
        [HttpPost]
        public async Task<ActionResult<Emociones>> Create([FromBody] Emociones emocion)
        {
            if (emocion == null)
                return BadRequest("Datos inválidos");

            var created = await _repository.CreateAsync(emocion);

            return CreatedAtAction(nameof(GetById), new { id = created.IdEmocion }, created);
        }

        // 📌 PUT: api/Emociones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Emociones emocion)
        {
            if (id != emocion.IdEmocion)
                return BadRequest("El ID no coincide");

            var updated = await _repository.UpdateAsync(emocion);

            if (!updated)
                return NotFound($"No se encontró la emoción con ID {id}");

            return NoContent();
        }

        // 📌 DELETE: api/Emociones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound($"No se encontró la emoción con ID {id}");

            return NoContent();
        }
    }
}