import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferenciaComercialComponent } from './referencia-comercial.component';

describe('ReferenciaComercialComponent', () => {
  let component: ReferenciaComercialComponent;
  let fixture: ComponentFixture<ReferenciaComercialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReferenciaComercialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferenciaComercialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
