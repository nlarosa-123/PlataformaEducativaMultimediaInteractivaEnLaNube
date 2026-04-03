import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiarioEmocionalIAComponent } from './diario-emocional-ia.component';

describe('DiarioEmocionalIAComponent', () => {
  let component: DiarioEmocionalIAComponent;
  let fixture: ComponentFixture<DiarioEmocionalIAComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DiarioEmocionalIAComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiarioEmocionalIAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
