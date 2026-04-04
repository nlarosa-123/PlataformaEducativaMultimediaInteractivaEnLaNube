import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistorialEmocionalComponent } from './historial-emocional.component';

describe('HistorialEmocionalComponent', () => {
  let component: HistorialEmocionalComponent;
  let fixture: ComponentFixture<HistorialEmocionalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HistorialEmocionalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HistorialEmocionalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
