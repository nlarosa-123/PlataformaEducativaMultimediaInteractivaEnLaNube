import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'; 


@Component({
  selector: 'app-modulos',
  standalone: true, 
  imports: [CommonModule,
    RouterModule
  ], 
  templateUrl: './modulos.component.html',
  styleUrl: './modulos.component.scss'
})
export class ModulosComponent implements OnInit {

  modulos: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerModulos();
  }

  obtenerModulos() {
    this.http.get<any[]>('http://localhost:5169/api/Modulos')
      .subscribe(data => {
        console.log(data);
        this.modulos = data;
      });
  }
}