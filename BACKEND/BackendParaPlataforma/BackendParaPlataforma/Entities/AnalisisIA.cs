public class AnalisisIA {

    public int Id_Analisis { get; set; }

    public int Id_Diario { get; set; }

    public int Emocion_Detectada_IA { get; set; }

    public string? Tono_Detectado { get; set; }

    public decimal Confianza { get; set; }

    public bool Coincide_Usuario { get; set; }

    public DateTime Fecha_Analisis { get; set; }
}