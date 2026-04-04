import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmocionesService {
  private apiUrl = 'http://localhost:5232/api/emociones';

  constructor(private http: HttpClient) { }

  obtenerEmociones(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`);
  }
}
