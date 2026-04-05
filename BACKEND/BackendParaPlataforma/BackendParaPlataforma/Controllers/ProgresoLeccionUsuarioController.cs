using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoLeccionUsuarioController : ControllerBase
    {
        private readonly IProgresoLeccionUsuarioRepository _repository;

        public ProgresoLeccionUsuarioController(IProgresoLeccionUsuarioRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/progresoleccionusuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var progresos = await _repository.GetAllAsync();
            return Ok(progresos);
        }

        // 🔹 GET: api/progresoleccionusuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var progreso = await _repository.GetByIdAsync(id);

            if (progreso == null)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(progreso);
        }

        // 🔹 GET: api/progresoleccionusuario/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var progresos = await _repository.GetByUsuarioAsync(usuarioId);
            return Ok(progresos);
        }

        // 🔹 GET: api/progresoleccionusuario/leccion/{leccionId}
        [HttpGet("leccion/{leccionId}")]
        public async Task<IActionResult> GetByLeccion(int leccionId)
        {
            var progresos = await _repository.GetByLeccionAsync(leccionId);
            return Ok(progresos);
        }

        // 🔹 GET: api/progresoleccionusuario/usuario/{usuarioId}/leccion/{leccionId}
        [HttpGet("usuario/{usuarioId}/leccion/{leccionId}")]
        public async Task<IActionResult> GetByUsuarioLeccion(int usuarioId, int leccionId)
        {
            var progreso = await _repository.GetByUsuarioLeccionAsync(usuarioId, leccionId);

            if (progreso == null)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(progreso);
        }

        // 🔹 POST: api/progresoleccionusuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgresoLeccionUsuario progreso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _repository.UpsertAsync(progreso);

                return Ok(result); // 👈 ya no Created siempre
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 🔹 PUT: api/progresoleccionusuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProgresoLeccionUsuario progreso)
        {
            if (id != progreso.Id_Progreso)
                return BadRequest(new { message = "El ID no coincide" });

            var updated = await _repository.UpdateAsync(progreso);

            if (!updated)
                return NotFound(new { message = "Progreso no encontrado" });

            return Ok(new { message = "Progreso actualizado correctamente" });
        }

        // 🔥 ENDPOINT CLAVE: Marcar lección como completada
        // PUT: api/progresoleccionusuario/completar
        [HttpPut("completar")]
        public async Task<IActionResult> CompletarLeccion(int usuarioId, int leccionId, int tiempoVisualizado)
        {
            var progreso = await _repository.GetByUsuarioLeccionAsync(usuarioId, leccionId);

            // 🔥 Si no existe, lo crea automáticamente
            if (progreso == null)
            {
                progreso = new ProgresoLeccionUsuario
                {
                    Id_Usuario = usuarioId,
                    Id_Leccion = leccionId,
                    Tiempo_Visualizado = tiempoVisualizado,
                    Completado = true,
                    Fecha_Completado = DateTime.UtcNow
                };

                await _repository.CreateAsync(progreso);
                return Ok(progreso);
            }

            // 🔥 Si ya existe, lo actualiza
            progreso.Completado = true;
            progreso.Tiempo_Visualizado = tiempoVisualizado;

            await _repository.UpdateAsync(progreso);

            return Ok(progreso);
        }

        // 🔹 DELETE: api/progresoleccionusuario/{id}
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
