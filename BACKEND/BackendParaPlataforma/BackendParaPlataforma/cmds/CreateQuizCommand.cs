namespace BackendParaPlataforma.cmds
{
    public class CreateQuizCommand
    {
        public int IdLeccion { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        //public List<PreguntaQuizCommand> Preguntas { get; set; }
    }
}
