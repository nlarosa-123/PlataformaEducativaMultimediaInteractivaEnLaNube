import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiarioEmocionalComponent } from './diario-emocional.component';

describe('DiarioEmocionalComponent', () => {
  let component: DiarioEmocionalComponent;
  let fixture: ComponentFixture<DiarioEmocionalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DiarioEmocionalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiarioEmocionalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
