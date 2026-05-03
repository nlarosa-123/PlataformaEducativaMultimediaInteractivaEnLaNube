import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  nombre = '';
  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    if (!this.nombre.trim() || !this.email.trim() || !this.password.trim()) {
      alert('Completa todos los campos.');
      return;
    }

    const emailValido = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.email);
    if (!emailValido) {
      alert('Introduce un email válido.');
      return;
    }

    const passwordValida = /^(?=.*[!@#$%^&*]).{6,}$/.test(this.password);
    if (!passwordValida) {
      alert('La contraseña debe tener al menos 6 caracteres y un caracter especial.');
      return;
    }

    this.authService.register(this.nombre, this.email, this.password)
      .subscribe({
        next: res => {
          console.log("Usuario creado", res);
          alert("Registro correcto");
          this.router.navigate(['/login']);
        },
        error: err => {
          console.error(err);
        }
      });
  }

}
