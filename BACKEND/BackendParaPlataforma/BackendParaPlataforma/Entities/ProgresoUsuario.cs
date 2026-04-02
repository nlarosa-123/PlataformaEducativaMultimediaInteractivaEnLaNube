using BackendParaPlataforma.Entities;

public class ProgresoUsuario {

    public int Id_Progreso { get; set; }

    public int Id_Usuario { get; set; }

    public string Modulo { get; set; }

    public int Porcentaje { get; set; }

    public DateTime Ultima_Actualizacion { get; set; }
    public Usuario? Usuario { get; set; }
}