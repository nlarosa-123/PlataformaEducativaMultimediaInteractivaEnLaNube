import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DiarioService {
  private apiUrl = 'http://localhost:5232/api/diario';

  constructor(private http: HttpClient) { }

  crearEntrada(entrada: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, entrada);
  }

  obtenerEntradas(usuarioId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/usuario/${usuarioId}`);
  }
}
