using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OpcionRespuesta
{
    [Key]
    public int IdOpcion { get; set; }

    [ForeignKey("Pregunta")]
    public int IdPregunta { get; set; }

    [Required]
    public string TextoOpcion { get; set; } = string.Empty;

    public bool EsCorrecta { get; set; }

    public PreguntaQuiz? Pregunta { get; set; }   // ← opcional
}
