namespace BackendParaPlataforma.dtos
{
    public class ProgresoLeccionesUsuarioDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdLeccion { get; set; }

        public bool Completada { get; set; }

        public DateTime FechaCompletada { get; set; }

        public int TiempoVisualizado { get; set; } //En segundos 
    }

}