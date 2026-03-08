import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { NavComponent } from './components/nav/nav.component';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  imports: [NavComponent, HomeComponent, HeaderComponent, RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  constructor(private authService: AuthService) {}

  login() {
    this.authService.login("email", "password")
      .subscribe(res => {
        console.log(res);
        localStorage.setItem('token', res.token);
      });
  }
  title = 'frontend';
}
