namespace BackendParaPlataforma.Entities
{
    public class DiarioEmocional
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int EmocionId { get; set; }

        public string Contenido { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        // Relaciones
        public Usuario? Usuario { get; set; }

        public Emociones? Emocion { get; set; }
    }
}
