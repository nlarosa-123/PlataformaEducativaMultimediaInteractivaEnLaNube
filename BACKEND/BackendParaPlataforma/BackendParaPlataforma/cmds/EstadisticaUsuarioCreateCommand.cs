namespace BackendParaPlataforma.cmds
{
    public class EstadisticaUsuarioCreateCommand
    {
        public int IdUsuario { get; set; }
        public int PorcentajeCoincidenciaIA { get; set; }
        public string EmocionMasFrecuente { get; set; }
        public int RachaDiasRegistrados { get; set; }
    }
}
