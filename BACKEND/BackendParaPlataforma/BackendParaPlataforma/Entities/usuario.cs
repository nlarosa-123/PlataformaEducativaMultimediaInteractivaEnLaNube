namespace BackendParaPlataforma.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }

        public string Nombre { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? UltimoLogin { get; private set; }

        public string? EstadoActual { get; private set; }

        public string? ConfiguracionVoz { get; private set; }

        public bool Activo { get; set; }
        public ICollection<ProgresoModuloUsuario> ProgresosModulos { get; set; }
        = new List<ProgresoModuloUsuario>();
        public ICollection<ProgresoUsuario> ProgresosUsuarios { get; set; }
        = new List<ProgresoUsuario>();

        public ICollection<DiarioEmocional> Diarios { get; set; }

        // Constructor principal
        public Usuario(string nombre, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("La contraseña es obligatoria");

            Nombre = nombre;
            Email = email;
            PasswordHash = passwordHash;
            FechaRegistro = DateTime.UtcNow;
            Activo = true;
        }

        // Constructor vacío requerido por EF Core
        private Usuario() { }

        public void RegistrarLogin()
        {
            UltimoLogin = DateTime.UtcNow;
        }

        public void CambiarEstado(string nuevoEstado)
        {
            EstadoActual = nuevoEstado;
        }

        public void CambiarConfiguracionVoz(string configuracion)
        {
            ConfiguracionVoz = configuracion;
        }

        public void Desactivar()
        {
            Activo = false;
        }

        public void Activar()
        {
            Activo = true;
        }
    }
}
