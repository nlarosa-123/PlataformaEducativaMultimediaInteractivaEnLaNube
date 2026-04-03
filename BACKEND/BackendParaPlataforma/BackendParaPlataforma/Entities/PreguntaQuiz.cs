using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PreguntaQuiz
{
    [Key]
    public int IdPregunta { get; set; }

    [ForeignKey("Quiz")]
    public int IdQuiz { get; set; }

    public string Pregunta { get; set; } = string.Empty;
    public int Orden { get; set; }

    public Quiz? Quiz { get; set; }   // ← opcional
    public ICollection<OpcionRespuesta> Opciones { get; set; } = new List<OpcionRespuesta>();
    public ICollection<RespuestaUsuarioQuiz> RespuestaUsuarioQuizzes { get; set; } = new List<RespuestaUsuarioQuiz>();
}

