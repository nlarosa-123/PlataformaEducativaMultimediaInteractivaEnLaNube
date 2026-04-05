using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulosController : ControllerBase
    {
        private readonly IModulosRepository _modulosRepository;

        public ModulosController(IModulosRepository modulosRepository)
        {
            _modulosRepository = modulosRepository;
        }

        // 🔹 GET: api/modulos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var modulos = await _modulosRepository.GetAllAsync();
            return Ok(modulos);
        }

        // 🔹 GET: api/modulos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var modulo = await _modulosRepository.GetByIdAsync(id);

            if (modulo == null)
                return NotFound(new { message = "Módulo no encontrado" });

            return Ok(modulo);
        }

        // 🔹 POST: api/modulos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Modulos modulo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _modulosRepository.CreateAsync(modulo);

            return CreatedAtAction(nameof(GetById), new { id = created.IdModulo }, created);
        }

        // 🔹 PUT: api/modulos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Modulos modulo)
        {
            if (id != modulo.IdModulo)
                return BadRequest(new { message = "El ID no coincide" });

            var updated = await _modulosRepository.UpdateAsync(modulo);

            if (!updated)
                return NotFound(new { message = "Módulo no encontrado" });

            return Ok(new { message = "Módulo actualizado correctamente" });
        }

        // 🔹 DELETE: api/modulos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _modulosRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Módulo no encontrado" });

            return Ok(new { message = "Módulo eliminado correctamente" });
        }
    }
}
