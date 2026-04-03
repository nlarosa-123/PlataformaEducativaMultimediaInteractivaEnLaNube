import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {

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
