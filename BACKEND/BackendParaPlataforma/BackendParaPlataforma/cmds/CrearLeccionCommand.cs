namespace BackendParaPlataforma.cmds
{
    public class CrearLeccionCommand
    {
        public string Titulo { get; set; } = string.Empty;
        public string TipoContenido { get; set; } = string.Empty;

        public string ContenidoTxt { get; set; } = string.Empty;

        public string UrlAudio { get; set; } = string.Empty;

        public string UrlVideo { get; set; } = string.Empty;

        public int Orden { get; set; }

        public int Duracion { get; set; }
    }

}