import { Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { MiespacioComponent } from './pages/miespacio/miespacio.component';
import { authGuard } from './guards/auth.guard';



export const routes: Routes = [
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'miespacio', component: MiespacioComponent, canActivate: [authGuard] }
];
