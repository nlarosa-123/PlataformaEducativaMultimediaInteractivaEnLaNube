using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;

[ApiController]
[Route("api/analisis")]
public class AnalisisIAController : ControllerBase {
    private readonly IAnalisisIARepository _repository;

    public AnalisisIAController(IAnalisisIARepository repository) {

        _repository = repository;
    }

    // Crear un nuevo Análisis
    [HttpPost]
    public async Task<IActionResult> GuardarAnalisis([FromBody] AnalisisIA analisis) {

        var resultado = await _repository.GuardarAnalisis(analisis);
        return Ok(resultado);
    }

    // Leer el análisis
    [HttpGet("{idDiario}")]
    public async Task<IActionResult> ObtenerAnalisis(int idDiario) {

        var analisis = await _repository.ObtenerAnalisisPorDiario(idDiario);

        if (analisis == null)
            return NotFound();

        return Ok(analisis);
    }

    // Actualizar análisis
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarAnalisis(int id, [FromBody] AnalisisIA analisis) {

        analisis.Id_Analisis = id;

        var actualizado = await _repository.ActualizarAnalisis(analisis);

        if (!actualizado)
            return NotFound();

        return Ok("Análisis actualizado");
    }

    // Eliminar análisis
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarAnalisis(int id) {

        var eliminado = await _repository.EliminarAnalisis(id);

        if (!eliminado)
            return NotFound();

        return Ok("Análisis eliminado");
    }
}
