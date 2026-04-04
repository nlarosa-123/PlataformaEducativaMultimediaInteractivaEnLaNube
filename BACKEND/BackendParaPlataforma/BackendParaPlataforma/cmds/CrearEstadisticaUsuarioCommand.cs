namespace BackendParaPlataforma.cmds
{
    public class CrearEstadisticaUsuarioCommand
    {
        public int IdUsuario { get; set; }

        public decimal PorcentajeCoincidenciaIA { get; set; }

        public string? EmocionMasFrecuente { get; set; }

        public int RachaDiasRegistrados { get; set; }
    }
}