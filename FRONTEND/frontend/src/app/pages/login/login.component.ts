import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    if (!this.email.trim() || !this.password.trim()) {
      alert('Completa el correo y la contraseña.');
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

    this.authService.login(this.email, this.password)
      .subscribe({
        next: res => {

          console.log(res);

          // guardar token
          localStorage.setItem('token', res.token);
          //guardar usuario
          localStorage.setItem('user', JSON.stringify(res.user));

          alert("Login correcto");

          // redirigir
          this.router.navigate(['/miespacio']);

        },
        error: err => {
          console.error(err);
          alert("Credenciales incorrectas");
        }
      });

  }
}
