namespace BackendParaPlataforma.dtos
{
    public class RespuestaUsuarioQuizDto
    {
        public int IdRespuesta { get; set; }
        public int IdUsuario { get; set; }
        public int IdPregunta { get; set; }
        public int IdOpcionElegida { get; set; }
        public bool Correcta { get; set; }
        public DateTime FechaRespuesta { get; set; }
    }
}
