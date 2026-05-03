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

  analisisList: any[] = [];
  azureAnalisis: any;
  tonoAzure: string = '';
  tonoAzureTexto: string = '';

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
  this.http.get<any[]>(`http://localhost:5169/api/SentimentResult/diario/${idDiario}`)
    .subscribe({
      next: (res) => {
        this.analisisList = res;

        // 🔥 separar Azure del resto
        this.azureAnalisis = res.find(x => x.provider === 'Azure');
        this.tonoAzure = this.azureAnalisis?.sentiment?.toLowerCase();
        this.tonoAzureTexto = this.getTextoTonoAzure();

        console.log(this.analisisList);
      },
      error: (err) => console.error(err)
    });
}

  //region actualizar analisisIA de Azure
  responderAzure(valor: boolean) {
  if (!this.azureAnalisis) return;

  const payload = {
    ...this.azureAnalisis,
    coincide_Usuario: valor,
    fecha_Analisis: new Date().toISOString()
  };

  this.http.put(
    `http://localhost:5169/api/SentimentResult/${this.azureAnalisis.id_Analisis}`,
    payload
  ).subscribe({
    next: () => {
      this.azureAnalisis.coincide_Usuario = valor;
    },
    error: (err) => {
  console.error(err);
}
  });
}

  getTextoTonoAzure(): string {
  switch (this.tonoAzure) {
    case 'positive':
      return 'positivo 😊';
    case 'neutral':
      return 'neutral 😐';
    case 'negative':
      return 'negativo 😞';
    case 'mixed':
      return 'mixto 🤔';
    default:
      return this.tonoAzure;
  }
}

  /* actualizarEstadistica() {
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
} */

}
