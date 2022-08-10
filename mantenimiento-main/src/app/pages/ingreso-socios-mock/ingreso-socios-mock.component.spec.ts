import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngresoSociosMockComponent } from './ingreso-socios-mock.component';

describe('IngresoSociosMockComponent', () => {
  let component: IngresoSociosMockComponent;
  let fixture: ComponentFixture<IngresoSociosMockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IngresoSociosMockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngresoSociosMockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
