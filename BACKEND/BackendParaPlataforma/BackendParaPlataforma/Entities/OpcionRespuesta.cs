public class OpcionRespuesta
{
    public int IdOpcion { get; set; }
    public int IdPregunta { get; set; }

    public string TextoOpcion { get; set; }
    public bool EsCorrecta { get; set; }

    public PreguntaQuiz Pregunta { get; set; }
}