import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EstadoFinancieroComponent } from './estado-financiero.component';

describe('EstadoFinancieroComponent', () => {
  let component: EstadoFinancieroComponent;
  let fixture: ComponentFixture<EstadoFinancieroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EstadoFinancieroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EstadoFinancieroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
