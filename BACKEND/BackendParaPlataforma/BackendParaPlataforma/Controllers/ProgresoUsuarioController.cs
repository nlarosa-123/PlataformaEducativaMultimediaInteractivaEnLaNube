using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoUsuarioController : ControllerBase
    {
        private readonly IProgresoUsuarioRepository _repository;

        public ProgresoUsuarioController(IProgresoUsuarioRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/progresousuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var progresos = await _repository.GetAllAsync();
            return Ok(progresos);
        }

        // 🔹 GET: api/progresousuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var progreso = await _repository.GetByIdAsync(id);

            if (progreso == null)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(progreso);
        }

        // 🔹 GET: api/progresousuario/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var progresos = await _repository.GetByUsuarioIdAsync(usuarioId);
            return Ok(progresos);
        }

        // 🔹 POST: api/progresousuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgresoUsuario progreso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.CreateAsync(progreso);

            return CreatedAtAction(nameof(GetById), new { id = created.Id_Progreso }, created);
        }

        // 🔹 PUT: api/progresousuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProgresoUsuario progreso)
        {
            if (id != progreso.Id_Progreso)
                return BadRequest(new { message = "El ID no coincide" });

            var updated = await _repository.UpdateAsync(progreso);

            if (!updated)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(new { message = "Progreso actualizado correctamente" });
        }

        // 🔹 DELETE: api/progresousuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(new { message = "Progreso eliminado correctamente" });
        }
    }
}
