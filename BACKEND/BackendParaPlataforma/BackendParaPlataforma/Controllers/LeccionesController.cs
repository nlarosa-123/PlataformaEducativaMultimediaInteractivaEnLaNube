using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeccionesController : ControllerBase
    {
        private readonly ILeccionesRepository _repository;

        public LeccionesController(ILeccionesRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/lecciones
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lecciones = await _repository.GetAllAsync();
            return Ok(lecciones);
        }

        // 🔹 GET: api/lecciones/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var leccion = await _repository.GetByIdAsync(id);

            if (leccion == null)
                return NotFound(new { message = "Lección no encontrada" });

            return Ok(leccion);
        }

        // 🔹 GET: api/lecciones/modulo/{moduloId}
        [HttpGet("modulo/{moduloId}")]
        public async Task<IActionResult> GetByModulo(int moduloId)
        {
            var lecciones = await _repository.GetByModuloIdAsync(moduloId);
            return Ok(lecciones);
        }

        // 🔹 POST: api/lecciones
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Lecciones leccion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 🔥 Validación básica de contenido
            if (string.IsNullOrEmpty(leccion.Titulo))
                return BadRequest("El título es obligatorio");

            var created = await _repository.CreateAsync(leccion);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // 🔹 PUT: api/lecciones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Lecciones leccion)
        {
            if (id != leccion.Id)
                return BadRequest(new { message = "El ID no coincide" });

            var updated = await _repository.UpdateAsync(leccion);

            if (!updated)
                return NotFound(new { message = "Lección no encontrada" });

            return Ok(new { message = "Lección actualizada correctamente" });
        }

        // 🔹 DELETE: api/lecciones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Lección no encontrada" });

            return Ok(new { message = "Lección eliminada correctamente" });
        }
        [HttpGet("{id}/siguiente")]
        public async Task<IActionResult> GetSiguiente(int id)
        {
            var actual = await _repository.GetByIdAsync(id);

            if (actual == null)
                return NotFound();

            var lecciones = await _repository.GetByModuloIdAsync(actual.IdModulo);

            var siguiente = lecciones
                .FirstOrDefault(l => l.Orden > actual.Orden);

            if (siguiente == null)
                return Ok(new { message = "Última lección" });

            return Ok(siguiente);
        }
    }
}
