using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;

[ApiController]
[Route("api/reflexion")]
public class ReflexionController : ControllerBase {
    private readonly IReflexionRepository _repository;

    public ReflexionController(IReflexionRepository repository) {

        _repository = repository;
    }

    // Crear una reflexion
    [HttpPost]
    public async Task<IActionResult> CrearReflexion([FromBody] ReflexionMejora reflexion) {

        var resultado = await _repository.CrearReflexion(reflexion);
        return Ok(resultado);
    }

    // Leer refleciones
    [HttpGet("{idDiario}")]
    public async Task<IActionResult> ObtenerReflexiones(int idDiario) {

        var reflexiones = await _repository.ObtenerReflexionesPorDiario(idDiario);
        return Ok(reflexiones);
    }

    // Actualizar reflexion
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarReflexion(int id, [FromBody] ReflexionMejora reflexion) {

        reflexion.Id_Reflexion = id;

        var actualizado = await _repository.ActualizarReflexion(reflexion);

        if (!actualizado)
            return NotFound();

        return Ok("Reflexión actualizada");
    }

    // Eliminar reflexion
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarReflexion(int id) {

        var eliminado = await _repository.EliminarReflexion(id);

        if (!eliminado)
            return NotFound();

        return Ok("Reflexión eliminada");
    }
}
