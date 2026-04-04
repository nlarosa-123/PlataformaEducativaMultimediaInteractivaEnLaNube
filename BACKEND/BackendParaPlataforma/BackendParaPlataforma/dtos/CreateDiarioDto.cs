namespace BackendParaPlataforma.Dtos {

    public class CreateDiarioDto {

        public int IdUsuario { get; set; }

        public DateTime Fecha { get; set; }

        public int IdEmocionUsuario { get; set; }

        public string TextoUsuario { get; set; }

        public string? AudioUrl { get; set; }
    }
}