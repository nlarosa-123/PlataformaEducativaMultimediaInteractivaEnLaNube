namespace BackendParaPlataforma.Entities
{
	public class Lecciones
	{
		public int Id { get; set; }

		public int IdModulo { get; set; } //Foreign Key 

		public string Titulo { get; set; } 

		//Igual deberia ser un Enum (Texto, video, audio, quiz) 
		public string TipoContenido { get; set; }

		public string ContenidoTxt { get; set; }

		public string UrlVideo { get; set; }

		public string UrlAudio { get; set; }

		public int Orden { get; set; } //Esto que es? 

		public int Duracion { get; set; } //En minutos 
        public ICollection<ProgresoLeccionUsuario> ProgresosUsuarios { get; set; }
		public Modulos Modulos { get; set; }
		public Quiz Quiz { get; set; }

        public Lecciones(string titulo, string texto, string urlV, string urlA, int duracion)
		{
			Titulo = titulo;
			ContenidoTxt = texto;
			UrlVideo = urlV;
			UrlAudio = urlA; 
			Duracion = duracion; 

			//Poner aqui lo de "Orden" si acaso 
		}

		//Constructor vacio 
		public Lecciones ()
		{

		}
	}
}