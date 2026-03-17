namespace BackendParaPlataforma.cmds
{
    public class CrearProgresoModuloUsuarioCommand
    {
        public int IdUsuario { get; set; }

        public int IdModulo { get; set; }

        public decimal Porcentaje { get; set; }

        public int? UltimaLeccion { get; set; }
    }
}
