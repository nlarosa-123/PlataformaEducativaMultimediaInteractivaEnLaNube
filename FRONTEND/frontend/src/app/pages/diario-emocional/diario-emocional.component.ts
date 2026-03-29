
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import RecordRTC from 'recordrtc';

@Component({
  selector: 'app-diario-emocional',
  templateUrl: './diario-emocional.component.html',
  styleUrls: ['./diario-emocional.component.scss']
})
export class DiarioEmocionalComponent {

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
}