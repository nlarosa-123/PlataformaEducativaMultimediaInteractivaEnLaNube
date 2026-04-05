import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/api/auth`;

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, {
      email,
      password
    });
  }

  register(nombre: string, email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, {
      nombre,
      email,
      password
    });
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('usuarioId');
  }

  setUsuarioId(usuarioId: number) {
    localStorage.setItem('usuarioId', usuarioId.toString());
  }

  getUsuarioId(): number | null {
    const id = localStorage.getItem('usuarioId');
    return id ? parseInt(id) : null;
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }
}