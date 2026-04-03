
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import RecordRTC from 'recordrtc';
import { FormsModule } from '@angular/forms';
import {  OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // 👈 FALTA ESTO


@Component({
  selector: 'app-diario-emocional',
  standalone: true, // 👈 IMPORTANTE
  imports: [FormsModule, CommonModule], // 👈 AQUÍ va FormsModule
  templateUrl: './diario-emocional.component.html',
  styleUrls: ['./diario-emocional.component.scss']
})
export class DiarioEmocionalComponent implements OnInit {

  ngOnInit(): void {
    this.cargarEmociones();
  }

  private recorder!: any;
  private stream!: MediaStream;

  transcription: string = '';

  constructor(private http: HttpClient) {}

  async startRecording() {
    this.stream = await navigator.mediaDevices.getUserMedia({ audio: true });

    this.recorder = new RecordRTC(this.stream, {
      type: 'audio',
      mimeType: 'audio/wav',
      recorderType: RecordRTC.StereoAudioRecorder,
      numberOfAudioChannels: 1,
      desiredSampRate: 16000
    });

    this.recorder.startRecording();

    console.log('🎤 Grabando...');
  }

  stopRecording() {
    this.recorder.stopRecording(() => {
      const blob = this.recorder.getBlob();

      console.log('🛑 Grabación detenida');

      this.sendAudioToBackend(blob);

      this.stream.getTracks().forEach(track => track.stop());
    });
  }

  sendAudioToBackend(audio: Blob) {
    const formData = new FormData();
    formData.append('file', audio, 'audio.wav');

    this.http.post<any>('http://localhost:5169/api/speech', formData)
      .subscribe({
        next: (res) => {
          this.transcription = res.text;
          console.log('🧠 Texto:', res.text);
        },
        error: (err) => console.error(err)
      });
  }
  //region guardar texto
  textoUsuario: string = '';
  emocionSeleccionada: number = 12; // puedes cambiar luego con UI
  guardarDiario() {
    const userId = this.getUserId();
    if (!userId) {
      alert("Usuario no autenticado");
      return;
    }
    const payload = {
      id_Usuario: userId,
      fecha: new Date().toISOString().split('T')[0],
      id_Emocion_Usuario: this.emocionSeleccionada,
      texto_Usuario: this.transcription || this.textoUsuario,
      audio_Url: null
    };

    this.http.post('http://localhost:5169/api/DiarioEmocional', payload)
      .subscribe({
        next: (res) => {
          console.log('✅ Diario guardado', res);

          // limpiar campos
          this.textoUsuario = '';
          this.transcription = '';
        },
        error: (err) => {
          console.error('❌ Error al guardar', err);
        }
      });
  }
  getUserId(): number {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user).id : 0;
  }
  //endregion guardar texto
  //region emociones
  emociones: any[] = [];
  cargarEmociones() {
  this.http.get<any[]>('http://localhost:5169/api/Emociones')
    .subscribe({
      next: (res) => {
        this.emociones = res;

        // seleccionar una por defecto (ej: neutral)
        if (this.emociones.length > 0) {
          this.emocionSeleccionada = this.emociones[0].idEmocion;
        }
      },
      error: (err) => {
        console.error('Error cargando emociones', err);
      }
    });
}
  //endregion emociones
}