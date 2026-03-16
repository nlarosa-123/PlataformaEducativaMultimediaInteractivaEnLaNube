namespace BackendParaPlataforma.Entities

public class Estadisticas
{
    public int Id { get;  set; } 

    public int IdUsuario { get;  set; }

    /*Este campo nos indica el porcentaje de coincidencia entre como te sientes en un día concreto 
      y lo que la IA ha detectado */ 
    public double CoincidenciaIA { get; set; }

    //El tipo que devuelve se cambiara mas tarde por un enum 
    public string EmocionFrecuente { get, set; }

    public int RachaDias { get,  set;  }

    public DateTime UltimaAct { get,  set;  }
    
    //Constructor principal (Preguntar) 
    public Estadisticas(int id, int idUsuario )
    { 
        IdUsuario = idUsuario; 
    }

    private Estadisticas() { } //Constructor vacio 

    public void ActualizarFecha()
    {
        UltimoAct = DateTime.UtcNow;
    }

    public void ActualizarRacha ()
    {
       RachaDias++;
    }

    public void CambiarEmocion (string nuevaEmocion)
    {
        EmocionFrecuente = nuevaEmocion; 
    }
}
