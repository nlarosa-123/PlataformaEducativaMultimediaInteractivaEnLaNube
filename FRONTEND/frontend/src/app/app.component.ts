import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { NavComponent } from './components/nav/nav.component';

@Component({
  selector: 'app-root',
  imports: [NavComponent, HomeComponent, HeaderComponent, RouterOutlet],
  //templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  template: `
   <app-nav></app-nav>
    <app-header></app-header>
   
    <router-outlet></router-outlet>
  
  `
})
export class AppComponent {
  title = 'frontend';
}
