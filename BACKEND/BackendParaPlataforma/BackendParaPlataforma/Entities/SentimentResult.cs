namespace BackendParaPlataforma.Entities
{
    public class SentimentResult
    {
        public int Id_Analisis { get; set; }

        public int Id_Diario { get; set; }

        //public int Emocion_Detectada_IA { get; set; }

        //public string? Tono_Detectado { get; set; }

        //public decimal Confianza { get; set; }

        public bool Coincide_Usuario { get; set; }
        public DateTime Fecha_Analisis { get; set; }
        public DiarioEmocional? DiarioEmocional { get; set; }
        //public Emociones? Emociones { get; set; }
        public string Provider { get; set; }
        public string Sentiment { get; set; }

        // Scores estándar (los que tú ya usas)
        public double? Positive { get; set; }
        public double? Neutral { get; set; }
        public double? Negative { get; set; }

        // Para Google u otros
        public double? Score { get; set; }       // -1 a 1
        public double? Magnitude { get; set; }   // intensidad

        // Para OpenAI o similares
        public double? Confidence { get; set; }
        public string? Explanation { get; set; }

        // Extra opcional (por si quieres guardar todo)
        public string? RawJson { get; set; }

    }
}
