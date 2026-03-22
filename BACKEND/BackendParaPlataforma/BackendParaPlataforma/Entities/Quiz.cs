using System.Collections.Generic;

public class Quiz
{
	public int IdQuiz { get; set; }
	public int IdLeccion { get; set; }

	public string Titulo { get; set; }
	public string Descripcion { get; set; }

	public ICollection<PreguntaQuiz> Preguntas { get; set; }
}