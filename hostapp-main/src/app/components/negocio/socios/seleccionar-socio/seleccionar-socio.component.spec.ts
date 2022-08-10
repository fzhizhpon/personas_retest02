import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeleccionarSocioComponent } from './seleccionar-socio.component';

describe('SeleccionarSocioComponent', () => {
  let component: SeleccionarSocioComponent;
  let fixture: ComponentFixture<SeleccionarSocioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeleccionarSocioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeleccionarSocioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
