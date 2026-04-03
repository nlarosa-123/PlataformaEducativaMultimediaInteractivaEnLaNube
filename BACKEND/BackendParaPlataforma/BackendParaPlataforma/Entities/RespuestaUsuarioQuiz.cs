using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendParaPlataforma.Entities;

public class RespuestaUsuarioQuiz
{
    [Key]
    public int IdRespuesta { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("Pregunta")]
    public int IdPregunta { get; set; }

    [ForeignKey("OpcionElegida")]
    public int IdOpcionElegida { get; set; }

    public bool Correcta { get; set; }
    public DateTime FechaRespuesta { get; set; } = DateTime.Now;

    public PreguntaQuiz? Pregunta { get; set; }        // ← opcional
    public OpcionRespuesta? OpcionElegida { get; set; } // ← opcional
    public Usuario? Usuario { get; set; }              // ← opcional
}
