namespace BackendParaPlataforma.Entities
{
    public class PreguntaQuiz
    {
        public int IdPregunta { get; set; }
        public int IdQuiz { get; set; }

        public string Pregunta { get; set; }
        public int Orden { get; set; }

        public Quiz Quiz { get; set; }
        public ICollection<OpcionRespuesta> Opciones { get; set; }
        public ICollection<RespuestaUsuarioQuiz> respuestaUsuarioQuizzes { get; set; }


    }
}

