
    namespace BackendParaPlataforma.Entities
    {
        public class ProgresoLeccionUsuario
        {
            public int Id_Progreso { get; set; }
            public int Id_Usuario { get; set; }
            public Usuario? Usuario { get; set; }
            public int Id_Leccion { get; set; }
            public Lecciones? Leccion { get; set; }
            public bool Completado { get; set; } = false;

            public DateTime? Fecha_Completado { get; set; }
            public int Tiempo_Visualizado { get; set; } = 0;
        }
    }