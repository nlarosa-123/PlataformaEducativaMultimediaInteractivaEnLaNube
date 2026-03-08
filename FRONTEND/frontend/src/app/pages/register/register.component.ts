import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  nombre = '';
  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  register() {
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
