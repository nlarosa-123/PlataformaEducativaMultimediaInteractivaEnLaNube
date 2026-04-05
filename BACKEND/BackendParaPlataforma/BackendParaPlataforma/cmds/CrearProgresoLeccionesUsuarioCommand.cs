namespace BackendParaPlataforma.cmds
{
    public class CrearProgresoLeccionesCommand
    { 
        public bool Completada { get; set; }

        public DateTime FechaCompletada { get; set; }

        public int TiempoVisualizado { get; set; } //En segundos

    }
}