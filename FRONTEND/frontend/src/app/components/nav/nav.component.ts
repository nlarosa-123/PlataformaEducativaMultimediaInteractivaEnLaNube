import { Component } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common'; // <--- IMPORTANTE para el *ngIf

@Component({
  selector: 'app-nav',
  standalone: true, // Asegúrate de que sea standalone si usas imports aquí
  imports: [RouterLink, CommonModule], // <--- Añade CommonModule aquí
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {
  constructor(private router: Router) {}

  // Esta función devolverá true si hay un token, false si no
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}