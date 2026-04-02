using BackendParaPlataforma.Entities;

public class ReflexionMejora {

    public int Id_Reflexion { get; set; }

    public int Id_Diario { get; set; }

    public string Texto_Reflexion { get; set; }

    public DateTime Fecha_Creacion { get; set; }
    public DiarioEmocional? DiarioEmocional { get; set; }
}