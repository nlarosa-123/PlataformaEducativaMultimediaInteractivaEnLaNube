using System;

namespace BackendParaPlataforma.Models
{
    public class RegistroSentimiento
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string ResultadoIA { get; set; }
        public bool CoincideUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}