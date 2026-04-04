import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-mi-espacio',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mi-espacio.component.html',
  styleUrl: './mi-espacio.component.scss'
})
export class MiEspacioComponent implements OnInit {

  nombreUsuario: string = '';

  estadoEmocional: string = '';
  emoji: string = '';
  tono: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const data = localStorage.getItem('user');

    if (data) {
      const parsed = JSON.parse(data);
      this.nombreUsuario = parsed.nombre;

      // 🔥 LLAMAR AL BACKEND
      this.cargarEstado(parsed.id);
    }
  }

  cargarEstado(userId: number) {
    this.http.get<any>(`http://localhost:5169/api/DiarioEmocional/usuario/${userId}/hoy`)
      .subscribe({
        next: (res) => {
          this.estadoEmocional = res.nombreEmocion;
          this.emoji = res.emoji;
          this.tono = res.tono;
        },
        error: (err) => {
          console.error('Error cargando estado emocional', err);
        }
      });
  }
}