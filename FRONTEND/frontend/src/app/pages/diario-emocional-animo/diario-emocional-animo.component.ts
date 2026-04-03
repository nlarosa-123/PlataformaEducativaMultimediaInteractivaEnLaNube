import { Component } from '@angular/core';
import RecordRTC from 'recordrtc';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-diario-emocional-animo',
  standalone: true,
  imports: [FormsModule, CommonModule], // 🔥 IMPORTANTE
  templateUrl: './diario-emocional-animo.component.html',
  styleUrl: './diario-emocional-animo.component.scss'
})
export class DiarioEmocionalAnimoComponent {

  textoUsuario: string = ''; // 🔥 AÑADIR
  transcription: string = '';

  private stream!: MediaStream;
  private recorder!: any;

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

  // 🔥 GUARDAR REFLEXIÓN
  guardarReflexion() {

    const idDiario = localStorage.getItem('idDiario');

    if (!idDiario) {
      alert('No hay diario asociado');
      return;
    }

    const payload = {
      id_Diario: Number(idDiario),
      texto_Reflexion: this.transcription || this.textoUsuario,
      fecha_Creacion: new Date().toISOString()
    };
    console.log("hola")
    console.log(payload.texto_Reflexion);

    this.http.post('http://localhost:5169/api/ReflexionMejora', payload)
      .subscribe({
        next: (res) => {
          console.log('✅ Reflexión guardada', res);
          alert('Reflexión guardada');

          // limpiar
          this.textoUsuario = '';
          this.transcription = '';
        },
        error: (err) => {
          console.error('❌ Error', err);
        }
      });
  }

}