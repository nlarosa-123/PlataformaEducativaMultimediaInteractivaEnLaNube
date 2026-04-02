namespace BackendParaPlataforma.Entities
{

    public class Quiz
    {
        public int IdQuiz { get; set; }
        public int IdLeccion { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public Lecciones Lecciones { get; set; }
        public ICollection<PreguntaQuiz> PreguntaQuizzes { get; set; }
    }
}
