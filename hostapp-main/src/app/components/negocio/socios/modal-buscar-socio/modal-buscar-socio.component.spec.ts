import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalBuscarSocioComponent } from './modal-buscar-socio.component';

describe('ModalBuscarSocioComponent', () => {
  let component: ModalBuscarSocioComponent;
  let fixture: ComponentFixture<ModalBuscarSocioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalBuscarSocioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalBuscarSocioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
