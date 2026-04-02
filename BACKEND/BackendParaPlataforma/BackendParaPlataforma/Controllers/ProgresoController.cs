using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;

[ApiController]
[Route("api/progreso")]
public class ProgresoController : ControllerBase {

    private readonly IProgresoRepository _repository;

    public ProgresoController(IProgresoRepository repository) {

        _repository = repository;
    }

    // Crear y guardar progreso
    [HttpPost]
    public async Task<IActionResult> GuardarProgreso([FromBody] ProgresoUsuario progreso) {

        var resultado = await _repository.GuardarProgreso(progreso);
        return Ok(resultado);
    }

    // Leer progrso
    [HttpGet("{idUsuario}/{modulo}")]
    public async Task<IActionResult> ObtenerProgreso(int idUsuario, string modulo) {

        var progreso = await _repository.ObtenerProgresoUsuario(idUsuario, modulo);

        if (progreso == null)
            return NotFound();

        return Ok(progreso);
    }

    // Actualizar progreso
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProgreso(int id, [FromBody] ProgresoUsuario progreso) {

        progreso.Id_Progreso = id;

        var actualizado = await _repository.ActualizarProgreso(progreso);

        if (!actualizado)
            return NotFound();

        return Ok("Progreso actualizado");
    }

    // Eliminar progreso
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProgreso(int id) {

        var eliminado = await _repository.EliminarProgreso(id);

        if (!eliminado)
            return NotFound();

        return Ok("Progreso eliminado");
    }
}