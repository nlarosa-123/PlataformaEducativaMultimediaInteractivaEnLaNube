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

    [HttpPost]
    public async Task<IActionResult> GuardarProgreso([FromBody] ProgresoUsuario progreso) {

        var resultado = await _repository.GuardarProgreso(progreso);
        return Ok(resultado);
    }

    [HttpGet("{idUsuario}/{modulo}")]
    public async Task<IActionResult> ObtenerProgreso(int idUsuario, string modulo) {

        var progreso = await _repository.ObtenerProgresoUsuario(idUsuario, modulo);

        if (progreso == null)
            return NotFound();

        return Ok(progreso);
    }
}