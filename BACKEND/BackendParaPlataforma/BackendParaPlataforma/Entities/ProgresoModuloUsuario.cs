namespace BackendParaPlataforma.Entities
{
    public class ProgresoModuloUsuario
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdModulo { get; set; }

        public decimal Porcentaje { get; set; }

        public bool Completado { get; set; }

        public int? UltimaLeccion { get; set; }
        public Usuario Usuario { get; set; }

        public ProgresoModuloUsuario() { }

        public ProgresoModuloUsuario(int idUsuario, int idModulo)
        {
            IdUsuario = idUsuario;
            IdModulo = idModulo;
            Porcentaje = 0;
            Completado = false;
        }

        public void ActualizarProgreso(decimal porcentaje, int ultimaLeccion)
        {
            Porcentaje = porcentaje;
            UltimaLeccion = ultimaLeccion;

            if (porcentaje >= 100)
            {
                Completado = true;
            }
        }
    }
}
