namespace BackendParaPlataforma.Entities
{
    public class Modulos
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        //Constructor principal 
        public Modulos(int id, string nombre, string descr)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descr;
        }

        //Constructor vacio 
        public Modulos() { }
    }
}