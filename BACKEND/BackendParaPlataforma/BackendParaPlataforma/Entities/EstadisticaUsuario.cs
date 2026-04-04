namespace BackendParaPlataforma.Entities
{
    public class EstadisticaUsuario
    {
        public int IdEstadistica { get; set; }   // PK

        public int IdUsuario { get; set; }       // FK

        public decimal PorcentajeCoincidenciaIA { get; set; }
        // decimal es mejor que float/double para precisión en porcentajes

        public string? EmocionMasFrecuente { get; set; }
        // nullable por si aún no hay datos

        public int RachaDiasRegistrados { get; set; }

        public DateTime UltimaActualizacion { get; set; }
        public Usuario? Usuario { get; set; }

        // Constructor opcional
        public EstadisticaUsuario() { }

        public EstadisticaUsuario(int idUsuario)
        {
            IdUsuario = idUsuario;
            UltimaActualizacion = DateTime.UtcNow;
            RachaDiasRegistrados = 0;
            PorcentajeCoincidenciaIA = 0;
        }

        // Método de dominio opcional (ejemplo)
        public void ActualizarEstadisticas(decimal porcentaje, string emocion, int racha)
        {
            PorcentajeCoincidenciaIA = porcentaje;
            EmocionMasFrecuente = emocion;
            RachaDiasRegistrados = racha;
            UltimaActualizacion = DateTime.UtcNow;
        }
    }
}