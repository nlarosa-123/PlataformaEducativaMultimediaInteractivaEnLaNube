namespace BackendParaPlataforma.Entities
{
    public class Modulos
    {
        // Clave primaria
        public int IdModulo { get; set; }

        // Título del módulo
        public string Titulo { get; set; }

        // Descripción del módulo
        public string Descripcion { get; set; }
        public List<Lecciones> Lecciones { get; set; }
        public List<ProgresoModuloUsuario> ProgresoModuloUsuarios { get; set; }

        //Constructor principal 
        public Modulos(int id, string nombre, string descr)
        {
            IdModulo = id;
            Titulo = nombre;
            Descripcion = descr;
        }

        //Constructor vacio 
        public Modulos() { }
    }
}