namespace BackendParaPlataforma.dtos
{
    public class QuizDto
    {
        public int IdQuiz { get; set; }
        public int IdLeccion { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public List<PreguntaQuizDto> Preguntas { get; set; }
    }
}
