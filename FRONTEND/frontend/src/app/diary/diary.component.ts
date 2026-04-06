import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DiarioService } from '../services/diary.service';
import { EmocionesService } from '../services/emociones.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-diary',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './diary.component.html',
  styleUrl: './diary.component.scss'
})
export class DiaryComponent implements OnInit {
  emociones: any[] = [];
  entradas: any[] = [];
  
  formulario = {
    emocionId: 0,
    contenido: ''
  };

  emocionSeleccionada: any = null;
  usuarioId: number | null = null;
  mensaje: string = '';
  tipoMensaje: 'exito' | 'error' | '' = '';
  cargando: boolean = false;

  // Propiedades para reconocimiento de voz
  reconocimientoActivado: boolean = false;
  grabando: boolean = false;
  reconocimiento: any = null;
  soportaVoz: boolean = false;
  private textoBase: string = '';

  constructor(
    private diarioService: DiarioService,
    private emocionesService: EmocionesService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.usuarioId = this.authService.getUsuarioId();
    this.cargarEmociones();
    if (this.usuarioId) {
      this.cargarEntradas();
    }
    this.inicializarReconocimientoVoz();
  }

  inicializarReconocimientoVoz() {
    const SpeechRecognition = (window as any).webkitSpeechRecognition || (window as any).SpeechRecognition;
    if (SpeechRecognition) {
      this.soportaVoz = true;
      this.reconocimiento = new SpeechRecognition();
      this.reconocimiento.lang = 'es-ES';
      this.reconocimiento.continuous = true;
      this.reconocimiento.interimResults = true;

      this.reconocimiento.onstart = () => {
        this.grabando = true;
      };

      this.reconocimiento.onend = () => {
        this.grabando = false;
      };

      this.reconocimiento.onresult = (event: any) => {
        let finalTranscript = '';
        let interimTranscript = '';

        for (let i = event.resultIndex; i < event.results.length; i++) {
          const transcript = event.results[i][0].transcript;
          if (event.results[i].isFinal) {
            finalTranscript += transcript;
          } else {
            interimTranscript += transcript;
          }
        }

        if (finalTranscript) {
          this.textoBase += finalTranscript + ' ';
        }
        this.formulario.contenido = this.textoBase + interimTranscript;
      };

      this.reconocimiento.onerror = (event: any) => {
        console.error('Error en reconocimiento:', event.error);
        this.mostrarMensaje('Error: ' + event.error, 'error');
      };
    }
  }

  toggleMicrofono() {
    if (!this.soportaVoz) {
      this.mostrarMensaje('Tu navegador no soporta reconocimiento de voz', 'error');
      return;
    }

    if (this.grabando) {
      this.reconocimiento.stop();
      this.grabando = false;
    } else {
      this.textoBase = '';
      this.formulario.contenido = '';
      this.reconocimiento.start();
    }
  }

  cargarEmociones() {
    this.emocionesService.obtenerEmociones().subscribe({
      next: (emociones) => {
        this.emociones = emociones;
      },
      error: (error) => {
        console.error('Error al cargar emociones:', error);
        this.mostrarMensaje('Error al cargar las emociones', 'error');
      }
    });
  }

  cargarEntradas() {
    if (!this.usuarioId) return;

    this.diarioService.obtenerEntradas(this.usuarioId).subscribe({
      next: (entradas) => {
        this.entradas = entradas;
      },
      error: (error) => {
        console.error('Error al cargar entradas:', error);
      }
    });
  }

  seleccionarEmocion(emocion: any) {
    this.emocionSeleccionada = emocion;
    this.formulario.emocionId = emocion.idEmocion || emocion.IdEmocion;
  }

  obtenerIdEmocion(emocion: any): number {
    return emocion.idEmocion || emocion.IdEmocion || 0;
  }

  obtenerEmocionPorId(id: number): any {
    return this.emociones.find(e => this.obtenerIdEmocion(e) === id);
  }

  guardarEntrada() {
    if (!this.formulario.emocionId) {
      this.mostrarMensaje('Por favor, selecciona una emoción', 'error');
      return;
    }

    if (!this.formulario.contenido.trim()) {
      this.mostrarMensaje('Por favor, escribe algo en tu diario', 'error');
      return;
    }

    if (!this.usuarioId) {
      this.mostrarMensaje('Error: Usuario no identificado', 'error');
      return;
    }

    this.cargando = true;

    const entrada = {
      usuarioId: this.usuarioId,
      emocionId: this.formulario.emocionId,
      contenido: this.formulario.contenido
    };

    this.diarioService.crearEntrada(entrada).subscribe({
      next: () => {
        this.mostrarMensaje('¡Entrada guardada correctamente!', 'exito');
        this.formulario.contenido = '';
        this.formulario.emocionId = 0;
        this.emocionSeleccionada = null;
        this.cargando = false;
        this.cargarEntradas();
      },
      error: (error) => {
        console.error('Error al guardar:', error);
        this.mostrarMensaje('Error al guardar la entrada', 'error');
        this.cargando = false;
      }
    });
  }

  mostrarMensaje(texto: string, tipo: 'exito' | 'error') {
    this.mensaje = texto;
    this.tipoMensaje = tipo;
    setTimeout(() => {
      this.mensaje = '';
      this.tipoMensaje = '';
    }, 3000);
  }

  formatearFecha(fecha: string): string {
    const date = new Date(fecha);
    return date.toLocaleDateString('es-ES', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }
}
