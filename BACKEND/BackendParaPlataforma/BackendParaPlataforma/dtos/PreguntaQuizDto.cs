namespace BackendParaPlataforma.dtos
{
    public class PreguntaQuizDto
    {
        public int IdPregunta { get; set; }
        public int IdQuiz { get; set; }

        public string Pregunta { get; set; }
        public int Orden { get; set; }

        public List<OpcionRespuestaDto> Opciones { get; set; }
    }
}
