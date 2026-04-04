import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface DiaCalendario {
  dia: number;
  fechaStr: string;
  entrada: any | null;
}

@Component({
  selector: 'app-historial-emocional',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './historial-emocional.component.html',
  styleUrl: './historial-emocional.component.scss'
})
export class HistorialEmocionalComponent implements OnInit {

  fechaSeleccionada: string = '';
  resultado: any = undefined; // undefined = no buscado; null = buscado sin resultado

  // --- Calendario ---
  mesActual: Date = new Date();
  diasDelMes: DiaCalendario[] = [];
  celdasVacias: null[] = [];
  cabeceraDias: string[] = ['L', 'M', 'X', 'J', 'V', 'S', 'D'];
  entradas: any[] = [];

  readonly MESES = [
    'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
    'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
  ];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.cargarTodasLasEntradas();
  }

  get nombreMes(): string {
    return `${this.MESES[this.mesActual.getMonth()]} ${this.mesActual.getFullYear()}`;
  }

  cargarTodasLasEntradas(): void {
    const user = localStorage.getItem('user');
    if (!user) return;
    const userId = JSON.parse(user).id;

    this.http.get<any[]>(`http://localhost:5169/api/DiarioEmocional/usuario/${userId}`)
      .subscribe({
        next: (res) => {
          // Normalizar claves para soportar tanto camelCase como PascalCase
          this.entradas = res.map(e => ({
            id_Diario:      e.id_Diario      ?? e.Id_Diario,
            fecha:          e.fecha          ?? e.Fecha,
            texto_Usuario:  e.texto_Usuario  ?? e.Texto_Usuario,
            nombreEmocion:  e.nombreEmocion  ?? e.NombreEmocion,
            emoji:          e.emoji          ?? e.Emoji,
          }));
          this.generarCalendario();
        },
        error: () => this.generarCalendario()
      });
  }

  generarCalendario(): void {
    const año = this.mesActual.getFullYear();
    const mes = this.mesActual.getMonth();
    const totalDias = new Date(año, mes + 1, 0).getDate();

    // Día de la semana del día 1 (0=Dom), convertido a índice lunes=0
    let primerDia = new Date(año, mes, 1).getDay();
    primerDia = primerDia === 0 ? 6 : primerDia - 1;

    this.celdasVacias = Array(primerDia).fill(null);

    this.diasDelMes = Array.from({ length: totalDias }, (_, i) => {
      const dia = i + 1;
      const fechaStr = `${año}-${String(mes + 1).padStart(2, '0')}-${String(dia).padStart(2, '0')}`;
      const entrada = this.entradas.find(e => e.fecha?.substring(0, 10) === fechaStr) ?? null;
      return { dia, fechaStr, entrada };
    });
  }

  mesAnterior(): void {
    this.mesActual = new Date(this.mesActual.getFullYear(), this.mesActual.getMonth() - 1, 1);
    this.generarCalendario();
  }

  mesSiguiente(): void {
    this.mesActual = new Date(this.mesActual.getFullYear(), this.mesActual.getMonth() + 1, 1);
    this.generarCalendario();
  }

  seleccionarDia(dia: DiaCalendario): void {
    this.fechaSeleccionada = dia.fechaStr;
    this.resultado = dia.entrada; // null si no hay; objeto si hay entrada
  }
}