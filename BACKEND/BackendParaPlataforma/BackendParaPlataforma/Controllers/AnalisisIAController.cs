using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Repositories;

[ApiController]
[Route("api/analisis")]
public class AnalisisIAController : ControllerBase
{
    private readonly IAnalisisIARepository _repository;

    public AnalisisIAController(IAnalisisIARepository repository) {

        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> GuardarAnalisis([FromBody] AnalisisIA analisis) {

        var resultado = await _repository.GuardarAnalisis(analisis);
        return Ok(resultado);
    }

    [HttpGet("{idDiario}")]
    public async Task<IActionResult> ObtenerAnalisis(int idDiario) {

        var analisis = await _repository.ObtenerAnalisisPorDiario(idDiario);

        if (analisis == null)
            return NotFound();

        return Ok(analisis);
    }
}