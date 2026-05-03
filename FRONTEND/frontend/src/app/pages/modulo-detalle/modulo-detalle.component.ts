import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-modulo-detalle',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './modulo-detalle.component.html',
  styleUrl: './modulo-detalle.component.scss'
})
export class ModuloDetalleComponent implements OnInit {
  readonly apiUrl = 'http://localhost:5169/api';

  lecciones: any[] = [];
  modulo: any = null;
  quizzes: Record<number, any> = {};
  preguntas: Record<number, any[]> = {};
  opciones: Record<number, any[]> = {};
  respuestas: Record<number, number> = {};
  leccionesCompletadas: Record<number, boolean> = {};

  idModulo = 0;
  cargando = true;
  error = '';

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.idModulo = Number(this.route.snapshot.params['id'] ?? 0);
    this.obtenerModulo();
    this.obtenerLecciones();
  }

  obtenerModulo(): void {
    this.http.get<any>(`${this.apiUrl}/Modulos/${this.idModulo}`).subscribe({
      next: (data) => {
        this.modulo = data;
      },
      error: () => {
        this.modulo = null;
      }
    });
  }

  obtenerLecciones(): void {
    this.cargando = true;
    this.error = '';

    this.http.get<any[]>(`${this.apiUrl}/Lecciones/modulo/${this.idModulo}`).subscribe({
      next: (data) => {
        this.lecciones = data ?? [];
        this.cargando = false;

        this.lecciones.forEach((leccion) => {
          this.obtenerQuiz(leccion.id);
        });
      },
      error: () => {
        this.lecciones = [];
        this.cargando = false;
        this.error = 'No se pudo cargar el contenido del módulo.';
      }
    });
  }

  obtenerQuiz(idLeccion: number): void {
    this.http.get<any>(`${this.apiUrl}/Quiz/leccion/${idLeccion}`).subscribe({
      next: (quiz) => {
        this.quizzes[idLeccion] = quiz;

        if (quiz?.idQuiz) {
          this.obtenerPreguntas(quiz.idQuiz);
        }
      },
      error: () => {
        this.quizzes[idLeccion] = null;
      }
    });
  }

  obtenerPreguntas(idQuiz: number): void {
    this.http.get<any[]>(`${this.apiUrl}/PreguntaQuiz/quiz/${idQuiz}`).subscribe({
      next: (data) => {
        this.preguntas[idQuiz] = data ?? [];

        this.preguntas[idQuiz].forEach((pregunta) => {
          this.obtenerOpciones(pregunta.idPregunta);
        });
      },
      error: () => {
        this.preguntas[idQuiz] = [];
      }
    });
  }

  obtenerOpciones(idPregunta: number): void {
    this.http.get<any[]>(`${this.apiUrl}/OpcionRespuesta/pregunta/${idPregunta}`).subscribe({
      next: (data) => {
        this.opciones[idPregunta] = data ?? [];
      },
      error: () => {
        this.opciones[idPregunta] = [];
      }
    });
  }

  esSeleccionada(idPregunta: number, idOpcion: number): boolean {
    return this.respuestas[idPregunta] === idOpcion;
  }

  yaRespondida(idPregunta: number): boolean {
    return this.respuestas[idPregunta] !== undefined;
  }

  esRespuestaCorrecta(idPregunta: number): boolean {
    return (this.opciones[idPregunta] ?? []).some(
      (opcion) => opcion.idOpcion === this.respuestas[idPregunta] && opcion.esCorrecta
    );
  }

  responder(pregunta: any, opcion: any): void {
    const user = JSON.parse(localStorage.getItem('user') || '{}');

    const payload = {
      idUsuario: user.id,
      idPregunta: pregunta.idPregunta,
      idOpcionElegida: opcion.idOpcion,
      correcta: opcion.esCorrecta
    };

    this.http.post(`${this.apiUrl}/RespuestaUsuarioQuiz`, payload).subscribe({
      next: () => {
        this.respuestas[pregunta.idPregunta] = opcion.idOpcion;
      },
      error: (err) => console.error(err)
    });
  }

  completarLeccion(idLeccion: number): void {
    const user = JSON.parse(localStorage.getItem('user') || '{}');

    const payload = {
      id_Usuario: user.id,
      id_Leccion: idLeccion,
      completado: true,
      fecha_Completado: new Date().toISOString(),
      tiempo_Visualizado: 120
    };

    this.http.post(`${this.apiUrl}/ProgresoLeccionUsuario`, payload).subscribe({
      next: () => {
        this.leccionesCompletadas[idLeccion] = true;
      },
      error: (err) => console.error(err)
    });
  }

  getQuizCount(): number {
    return Object.values(this.quizzes).filter(Boolean).length;
  }

  getCompletedCount(): number {
    return Object.values(this.leccionesCompletadas).filter(Boolean).length;
  }

  getProgressPercent(): number {
    if (!this.lecciones.length) {
      return 0;
    }

    return Math.round((this.getCompletedCount() / this.lecciones.length) * 100);
  }
}