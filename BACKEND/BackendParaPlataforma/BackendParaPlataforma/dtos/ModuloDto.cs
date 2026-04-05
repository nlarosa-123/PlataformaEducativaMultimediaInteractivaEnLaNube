namespace BackendParaPlataforma.dtos
{
    public class ModuloDto
    {
        public int IdModulo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<LeccionDto> Lecciones { get; set; }
    }
}
