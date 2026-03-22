using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Repositories;

[ApiController]
[Route("api/reflexion")]
public class ReflexionController : ControllerBase {
    private readonly IReflexionRepository _repository;

    public ReflexionController(IReflexionRepository repository) {

        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CrearReflexion([FromBody] ReflexionMejora reflexion) {

        var resultado = await _repository.CrearReflexion(reflexion);
        return Ok(resultado);
    }

    [HttpGet("{idDiario}")]
    public async Task<IActionResult> ObtenerReflexiones(int idDiario) {

        var reflexiones = await _repository.ObtenerReflexionesPorDiario(idDiario);
        return Ok(reflexiones);
    }
}