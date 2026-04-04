import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-inteligencia-artificial',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './inteligencia-artificial.component.html',
  styleUrl: './inteligencia-artificial.component.scss'
})
export class InteligenciaArtificialComponent implements OnInit {

  estadistica: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.cargarEstadistica();
  }

  cargarEstadistica() {
    const user = localStorage.getItem('user');
    if (!user) return;

    const userId = JSON.parse(user).id;

    this.http.get<any>(`http://localhost:5169/api/EstadisticaUsuario/usuario/${userId}`)
      .subscribe({
        next: (res) => {
          this.estadistica = res;
        },
        error: (err) => console.error(err)
      });
  }

}