import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common'; 
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-modulo-detalle',
  imports: [
    CommonModule,  
    RouterModule 
  ],
  templateUrl: './modulo-detalle.component.html',
  styleUrl: './modulo-detalle.component.scss'
})
export class ModuloDetalleComponent implements OnInit {

  lecciones: any[] = [];
  quizzes: { [key: number]: any } = {}; 
  idModulo!: number;
  preguntas: { [key: number]: any[] } = {}; 
  opciones: { [key: number]: any[] } = {};

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.idModulo = this.route.snapshot.params['id'];
    this.obtenerLecciones();
  }

  obtenerLecciones() {
    this.http.get<any[]>(`http://localhost:5169/api/Lecciones/modulo/${this.idModulo}`)
      .subscribe(data => {
        this.lecciones = data;

        // 🔥 cargar quiz por cada lección
        this.lecciones.forEach(leccion => {
          this.obtenerQuiz(leccion.id);
        });
      });
  }

  obtenerQuiz(idLeccion: number) {
  this.http.get<any>(`http://localhost:5169/api/Quiz/leccion/${idLeccion}`)
    .subscribe(quiz => {
      this.quizzes[idLeccion] = quiz;

      // 🔥 ahora cargar preguntas con el id del quiz
      if (quiz?.idQuiz) {
        this.obtenerPreguntas(quiz.idQuiz);
      }
    });
  }

  obtenerPreguntas(idQuiz: number) {
  this.http.get<any[]>(`http://localhost:5169/api/PreguntaQuiz/quiz/${idQuiz}`)
    .subscribe(data => {
      this.preguntas[idQuiz] = data;

      // 🔥 cargar opciones por cada pregunta
      data.forEach(p => {
        this.obtenerOpciones(p.idPregunta);
      });
    });
}
  obtenerOpciones(idPregunta: number) {
  this.http.get<any[]>(`http://localhost:5169/api/OpcionRespuesta/pregunta/${idPregunta}`)
    .subscribe(data => {
      this.opciones[idPregunta] = data;
    });
}
}