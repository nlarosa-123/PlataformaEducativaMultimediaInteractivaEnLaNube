import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DiaryComponent } from './diary/diary.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { MiespacioComponent } from './pages/miespacio/miespacio.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'diary', component: DiaryComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  //{ path: 'miespacio', component: MiespacioComponent, canActivate: [authGuard] }
];
