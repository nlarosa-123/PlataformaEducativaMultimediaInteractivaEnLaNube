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

        //Constructor principal 
        public Modulos(string nombre, string descr)
        {
            Titulo = nombre;
            Descripcion = descr;
        }

        //Constructor vacio 
        public Modulos() { }
    }
}