import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiespacioComponent } from './miespacio.component';

describe('MiespacioComponent', () => {
  let component: MiespacioComponent;
  let fixture: ComponentFixture<MiespacioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MiespacioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MiespacioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
