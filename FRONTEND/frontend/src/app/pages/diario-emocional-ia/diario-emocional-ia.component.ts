import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-diario-emocional-ia',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './diario-emocional-ia.component.html',
  styleUrl: './diario-emocional-ia.component.scss'
})
export class DiarioEmocionalIAComponent implements OnInit {

  analisis: any;
  tono: string = '';

  constructor(private http: HttpClient, private route: ActivatedRoute) {}

  ngOnInit(): void {
      const id = localStorage.getItem('idDiario');

      if (!id) {
        console.error('No hay idDiario');
        return;
      }

      this.cargarAnalisis(Number(id));
      console.log(Number(id));
  }

  cargarAnalisis(idDiario: number) {
    this.http.get<any[]>(`http://localhost:5169/api/AnalisisIA/diario/${idDiario}`)
      .subscribe({
        next: (res) => {
          if (res.length > 0) {
            this.analisis = res[0];
            this.tono = this.analisis.tono_Detectado.toLowerCase();
          }
        },
        error: (err) => console.error(err)
      });
      console.log(this.analisis);
      console.log(this.tono);
  }

  //region actualizar analisisIA
  responder(valor: boolean) {
  if (!this.analisis) return;

  const payload = {
    id_Analisis: this.analisis.id_Analisis,
    id_Diario: this.analisis.id_Diario,
    emocion_Detectada_IA: this.analisis.emocion_Detectada_IA,
    tono_Detectado: this.analisis.tono_Detectado,
    confianza: this.analisis.confianza,
    coincide_Usuario: valor,
    fecha_Analisis: new Date().toISOString()
  };

  this.http.put(`http://localhost:5169/api/AnalisisIA/${this.analisis.id_Analisis}`, payload)
    .subscribe({
      next: () => {
        console.log('✅ Respuesta guardada');

        //NUEVO: actualizar estadísticas
        this.actualizarEstadistica();

        // opcional: feedback visual
        alert('Respuesta guardada correctamente');
      },
      error: (err) => {
        console.error('❌ Error actualizando análisis', err);
      }
    });
}
  //endregion actualizar analisisIA
    getTextoTono(): string {
    switch (this.tono) {
      case 'positivo':
        return 'positivo 😊';
      case 'neutral':
        return 'neutral 😐';
      case 'negativo':
        return 'negativo 😞';
      case 'mixto':
        return 'mixto 🤔';
      default:
        return this.tono;
    }
  }

  actualizarEstadistica() {
  const idUsuario = 1002;

  this.http.post(
    `http://localhost:5169/api/EstadisticaUsuario/actualizar/${idUsuario}`,
    {}, // 👈 vacío
    { responseType: 'text' } // 👈 porque backend devuelve string
  )
  .subscribe({
    next: (res) => {
      console.log('📊 Estadística actualizada', res);
    },
    error: (err) => console.error(err)
  });
}

}
