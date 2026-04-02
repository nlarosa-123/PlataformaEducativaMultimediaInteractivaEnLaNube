namespace BackendParaPlataforma.Entities
{
    public class RespuestaUsuarioQuiz
    {
        public int IdRespuesta { get; set; }
        public int IdUsuario { get; set; }
        public int IdPregunta { get; set; }
        public int IdOpcionElegida { get; set; }

        public bool Correcta { get; set; }
        public DateTime FechaRespuesta { get; set; }

        public PreguntaQuiz Pregunta { get; set; }
        public Usuario Usuario { get; set; }
        public OpcionRespuesta OpcionRespuesta { get; set; }
    }
}
