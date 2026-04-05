// import { ComponentFixture, TestBed } from '@angular/core/testing';

// import { DiaryComponent } from './diary.component';

// describe('DiaryComponent', () => {
//   let component: DiaryComponent;
//   let fixture: ComponentFixture<DiaryComponent>;

//   beforeEach(async () => {
//     await TestBed.configureTestingModule({
//       imports: [DiaryComponent]
//     })
//     .compileComponents();

//     fixture = TestBed.createComponent(DiaryComponent);
//     component = fixture.componentInstance;
//     fixture.detectChanges();
//   });

//   it('should create', () => {
//     expect(component).toBeTruthy();
//   });
// });
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DiaryComponent } from './diary.component';
import { DiarioService } from '../services/diary.service';
import { EmocionesService } from '../services/emociones.service';
import { AuthService } from '../services/auth.service';
import { of } from 'rxjs';

const diarioServiceMock = {
  crearEntrada: () => of({}),
  obtenerEntradas: () => of([])
};

const emocionesServiceMock = {
  obtenerEmociones: () => of([])
};

const authServiceMock = {
  getUsuarioId: () => null,
  getToken: () => null
};

describe('DiaryComponent', () => {
  let component: DiaryComponent;
  let fixture: ComponentFixture<DiaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DiaryComponent],
      providers: [
        { provide: DiarioService, useValue: diarioServiceMock },
        { provide: EmocionesService, useValue: emocionesServiceMock },
        { provide: AuthService, useValue: authServiceMock }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});