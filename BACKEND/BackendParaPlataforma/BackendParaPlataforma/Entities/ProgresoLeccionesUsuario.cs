namespace BackendParaPlataforma.Entities
{
    public class ProgresoLeccionesUsuario
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdLeccion { get; set; }

        public bool Completada { get; set; }

        public DateTime FechaCompletada { get; set; }

        public int TiempoVisualizado { get; set; } //En segundos 


        //Constructor vacio 
        public ProgresoLeccionesUsuario () {
            Completada = false;
            TiempoVisualizado = 0; 
        }

        public void LeccionCompletada()
        {
            Completada = true; 
        }

        public void SetFecha ()
        {
            FechaCompletada = DateTime.UtcNow;
        }
    }

}