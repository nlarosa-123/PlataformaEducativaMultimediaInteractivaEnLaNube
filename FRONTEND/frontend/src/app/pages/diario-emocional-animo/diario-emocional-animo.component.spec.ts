import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiarioEmocionalAnimoComponent } from './diario-emocional-animo.component';

describe('DiarioEmocionalAnimoComponent', () => {
  let component: DiarioEmocionalAnimoComponent;
  let fixture: ComponentFixture<DiarioEmocionalAnimoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DiarioEmocionalAnimoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiarioEmocionalAnimoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
