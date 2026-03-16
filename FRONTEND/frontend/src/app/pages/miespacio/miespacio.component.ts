import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-miespacio',
  imports: [],
  templateUrl: './miespacio.component.html',
  styleUrl: './miespacio.component.scss'
})
export class MiespacioComponent {

  constructor(private router: Router) {}

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
