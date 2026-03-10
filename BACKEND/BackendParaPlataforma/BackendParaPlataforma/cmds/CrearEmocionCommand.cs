namespace BackendParaPlataforma.cmds
{
    public class CrearEmocionCommand
    {
        public string Nombre { get; set; } = string.Empty;

        public string Emoji { get; set; } = string.Empty;

        public decimal? Valor { get; set; }
    }
}
