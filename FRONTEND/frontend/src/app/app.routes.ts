import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DiaryComponent } from './diary/diary.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { authGuard } from './guards/auth.guard';
import { DiarioEmocionalComponent } from './pages/diario-emocional/diario-emocional.component';
import { DiarioEmocionalIAComponent } from './pages/diario-emocional-ia/diario-emocional-ia.component';
import { DiarioEmocionalAnimoComponent } from './pages/diario-emocional-animo/diario-emocional-animo.component';
import { MiEspacioComponent } from './pages/mi-espacio/mi-espacio.component';
import { HistorialEmocionalComponent } from './pages/historial-emocional/historial-emocional.component';
import { InteligenciaArtificialComponent } from './pages/inteligencia-artificial/inteligencia-artificial.component';
import { ModulosComponent } from './pages/modulos/modulos.component';
import { ModuloDetalleComponent } from './pages/modulo-detalle/modulo-detalle.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'diary', component: DiaryComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'miespacio', component: MiEspacioComponent, canActivate: [authGuard] },
  { path: 'diarioemocional', component: DiarioEmocionalComponent, canActivate: [authGuard] },
  { path: 'diarioemocionalia', component: DiarioEmocionalIAComponent, canActivate: [authGuard] },
  { path: 'diarioemocionalanimo', component: DiarioEmocionalAnimoComponent, canActivate: [authGuard] },
  { path: 'historialemocional', component: HistorialEmocionalComponent, canActivate: [authGuard] },
  { path: 'inteligenciaartificial', component: InteligenciaArtificialComponent, canActivate: [authGuard] },
  { path: 'modulos', component: ModulosComponent, canActivate: [authGuard]},
  { path: 'modulos/:id', component: ModuloDetalleComponent }
];
