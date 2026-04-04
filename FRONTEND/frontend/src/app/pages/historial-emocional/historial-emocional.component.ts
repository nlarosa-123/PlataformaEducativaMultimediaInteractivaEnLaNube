import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-historial-emocional',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './historial-emocional.component.html',
  styleUrl: './historial-emocional.component.scss'
})
export class HistorialEmocionalComponent {

  fechaSeleccionada: string = '';
  resultado: any = null;

  constructor(private http: HttpClient) {}

  buscar() {
    const user = localStorage.getItem('user');
    if (!user) return;

    const userId = JSON.parse(user).id;

    this.http.get<any[]>(
      `http://localhost:5169/api/DiarioEmocional/usuario/${userId}/fecha?fecha=${this.fechaSeleccionada}`
    )
    .subscribe({
      next: (res) => {
        this.resultado = res.length > 0 ? res[0] : null;
      },
      error: (err) => {
        console.error(err);
        this.resultado = null;
      }
    });
  }
}