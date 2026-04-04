namespace BackendParaPlataforma.dtos
{
    public class EstadisticaUsuarioDto
    {
        public int IdEstadistica { get; set; }

        public int IdUsuario { get; set; }

        public decimal PorcentajeCoincidenciaIA { get; set; }

        public string? EmocionMasFrecuente { get; set; }

        public int RachaDiasRegistrados { get; set; }

        public DateTime UltimaActualizacion { get; set; }
    }
}
