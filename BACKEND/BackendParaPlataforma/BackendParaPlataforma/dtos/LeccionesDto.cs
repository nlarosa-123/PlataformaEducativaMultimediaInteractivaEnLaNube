namespace BackendParaPlataforma.dtos
{
    public class LeccionesDto
    {
        public int Id { get; set; }

        public int IdModulo { get; set; } //Foreign Key 

        public string Titulo { get; set; } = string.Empty;

        //Igual deberia ser un Enum (Texto, video, audio, quiz) 
        public string TipoContenido { get; set; } = string.Empty;

        public string ContenidoTxt { get; set; } = string.Empty;

        public string UrlVideo { get; set; } = string.Empty;

        public string UrlAudio { get; set; } = string.Empty; 

        public int Orden { get; set; } //Esto que es? 

        public int Duracion { get; set; } //En minutos 
    }

}