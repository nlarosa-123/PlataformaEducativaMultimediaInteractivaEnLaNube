namespace BackendParaPlataforma.dtos
{
    public class ProgresoModuloUsuarioDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int IdModulo { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Completado { get; set; }
        public int? UltimaLeccion { get; set; }
    }
}
