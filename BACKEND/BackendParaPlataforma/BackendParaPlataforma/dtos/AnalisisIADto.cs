namespace BackendParaPlataforma.Dtos {

    public class AnalisisIADto {

        public int IdDiario { get; set; }

        public int EmocionDetectadaIA { get; set; }

        public string TonoDetectado { get; set; }

        public decimal Confianza { get; set; }

        public bool CoincideUsuario { get; set; }
    }
}