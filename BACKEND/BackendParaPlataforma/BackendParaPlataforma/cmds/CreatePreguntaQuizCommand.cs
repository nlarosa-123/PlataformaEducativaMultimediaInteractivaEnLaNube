namespace BackendParaPlataforma.cmds
{
    public class CreatePreguntaQuizCommand
    {
        public int IdQuiz { get; set; }
        public string Pregunta { get; set; }
        public int Orden { get; set; }

        //public List<OpcionRespuestaCommand> Opciones { get; set; }
    }
}