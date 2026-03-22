using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendParaPlataforma.cmds;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizRepository _repository;

    public QuizController(IQuizRepository repository)
    {
        _repository = repository;
    }

    // ? CREATE
    [HttpPost]
    public async Task<ActionResult<QuizDto>> Create(CreateQuizCommand command)
    {
        var quiz = new Quiz
        {
            IdLeccion = command.IdLeccion,
            Titulo = command.Titulo,
            Descripcion = command.Descripcion,
            Preguntas = command.Preguntas?.Select(p => new PreguntaQuiz
            {
                Texto = p.Texto
            }).ToList()
        };

        var id = await _repository.CreateAsync(quiz);

        var result = MapToDto(quiz);

        return CreatedAtAction(nameof(GetById), new { id }, result);
    }

    // ? READ BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizDto>> GetById(int id)
    {
        var quiz = await _repository.GetByIdAsync(id);

        if (quiz == null)
            return NotFound();

        return Ok(MapToDto(quiz));
    }

    // ? READ ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizDto>>> GetAll()
    {
        var quizzes = await _repository.GetAllAsync();

        return Ok(quizzes.Select(q => MapToDto(q)));
    }

    // ? UPDATE
    [HttpPut("{id}")]
    public async Task<ActionResult<QuizDto>> Update(int id, CreateQuizCommand command)
    {
        var quiz = await _repository.GetByIdAsync(id);

        if (quiz == null)
            return NotFound();

        quiz.IdLeccion = command.IdLeccion;
        quiz.Titulo = command.Titulo;
        quiz.Descripcion = command.Descripcion;

        // Manejo simple de preguntas (reemplazo completo)
        quiz.Preguntas = command.Preguntas?.Select(p => new PreguntaQuiz
        {
            Texto = p.Texto
        }).ToList();

        await _repository.UpdateAsync(quiz);

        return Ok(MapToDto(quiz));
    }

    // ? DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var quiz = await _repository.GetByIdAsync(id);

        if (quiz == null)
            return NotFound();

        await _repository.DeleteAsync(id);

        return NoContent();
    }

    // ?? Mapper manual (Entidad ? DTO)
    private QuizDto MapToDto(Quiz quiz)
    {
        return new QuizDto
        {
            IdQuiz = quiz.IdQuiz,
            IdLeccion = quiz.IdLeccion,
            Titulo = quiz.Titulo,
            Descripcion = quiz.Descripcion,
            Preguntas = quiz.Preguntas?.Select(p => new PreguntaQuizDto
            {
                IdPreguntaQuiz = p.IdPreguntaQuiz,
                Texto = p.Texto
            }).ToList()
        };
    }
}