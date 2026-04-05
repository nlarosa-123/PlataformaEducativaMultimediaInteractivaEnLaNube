import { ComponentFixture, TestBed } from '@angular/core/testing';

<<<<<<<< HEAD:FRONTEND/frontend/src/app/pages/modulos/modulos.component.spec.ts
import { ModulosComponent } from './modulos.component';

describe('ModulosComponent', () => {
  let component: ModulosComponent;
  let fixture: ComponentFixture<ModulosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModulosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModulosComponent);
========
import { MiEspacioComponent } from './mi-espacio.component';

describe('MiEspacioComponent', () => {
  let component: MiEspacioComponent;
  let fixture: ComponentFixture<MiEspacioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MiEspacioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MiEspacioComponent);
>>>>>>>> ire:FRONTEND/frontend/src/app/pages/mi-espacio/mi-espacio.component.spec.ts
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
