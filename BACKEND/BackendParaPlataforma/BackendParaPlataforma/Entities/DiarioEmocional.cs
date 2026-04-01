namespace BackendParaPlataforma.Entities {

    public class DiarioEmocional {

        public int Id_Diario { get; set; }

        public int Id_Usuario { get; set; }

        public Usuario? Usuario { get; set; }

        public DateTime Fecha { get; set; }

        public int Id_Emocion_Usuario  { get; set; }

        public Emociones? Emocion { get; set; }

        public string? Texto_Usuario { get; set; }

        public string? Audio_Url { get; set; }

        public DateTime Fecha_Creacion { get; set; }
    }
}