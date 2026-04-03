import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DiaryComponent } from './diary/diary.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { MiespacioComponent } from './pages/miespacio/miespacio.component';
import { authGuard } from './guards/auth.guard';
import { DiarioEmocionalComponent } from './pages/diario-emocional/diario-emocional.component';
import { DiarioEmocionalIAComponent } from './pages/diario-emocional-ia/diario-emocional-ia.component';
import { DiarioEmocionalAnimoComponent } from './pages/diario-emocional-animo/diario-emocional-animo.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'diary', component: DiaryComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'miespacio', component: MiespacioComponent, canActivate: [authGuard] },
  { path: 'diarioemocional', component: DiarioEmocionalComponent, canActivate: [authGuard] },
  { path: 'diarioemocionalia', component: DiarioEmocionalIAComponent, canActivate: [authGuard] },
  { path: 'diarioemocionalanimo', component: DiarioEmocionalAnimoComponent, canActivate: [authGuard] }
];
