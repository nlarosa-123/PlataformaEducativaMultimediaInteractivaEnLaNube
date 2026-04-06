using BackendParaPlataforma.cmds;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoModuloUsuarioController : ControllerBase
    {
        private readonly IProgresoModuloUsuarioRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProgresoModuloUsuarioController(IProgresoModuloUsuarioRepository repository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        // 🔹 GET: api/progresomodulousuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var progresos = await _repository.GetAllAsync();
            return Ok(progresos);
        }

        // 🔹 GET: api/progresomodulousuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var progreso = await _repository.GetByIdAsync(id);

            if (progreso == null)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(progreso);
        }

        // 🔹 GET: api/progresomodulousuario/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var progresos = await _repository.GetByUsuarioIdAsync(usuarioId);
            return Ok(progresos);
        }

        // 🔹 GET: api/progresomodulousuario/usuario/{usuarioId}/modulo/{moduloId}
        [HttpGet("usuario/{usuarioId}/modulo/{moduloId}")]
        public async Task<IActionResult> GetByUsuarioModulo(int usuarioId, int moduloId)
        {
            var progreso = await _repository.GetByUsuarioModuloAsync(usuarioId, moduloId);

            if (progreso == null)
                return NotFound(new { message = "Progreso no encontrado para este usuario y módulo" });

            return Ok(progreso);
        }

        // 🔹 POST: api/progresomodulousuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgresoModuloUsuario progreso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.CreateAsync(progreso);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // 🔹 PUT: api/progresomodulousuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProgresoModuloUsuario progreso)
        {
            if (id != progreso.Id)
                return BadRequest(new { message = "El ID no coincide" });

            var updated = await _repository.UpdateAsync(progreso);

            if (!updated)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(new { message = "Progreso actualizado correctamente" });
        }

        // 🔹 PUT: api/progresomodulousuario/actualizar-progreso
        // 👉 endpoint más "inteligente"
        [HttpPut("actualizar-progreso")]
        public async Task<IActionResult> ActualizarProgreso(
            int usuarioId,
            int moduloId,
            decimal porcentaje,
            int ultimaLeccion)
        {
            var progreso = await _repository.GetByUsuarioModuloAsync(usuarioId, moduloId);

            if (progreso == null)
                return NotFound(new { message = "Progreso no encontrado" });

            progreso.ActualizarProgreso(porcentaje, ultimaLeccion);

            await _repository.UpdateAsync(progreso);

            return Ok(progreso);
        }

        // 🔹 DELETE: api/progresomodulousuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(new { message = "Progreso eliminado correctamente" });
        }

        #endregion
    }
}