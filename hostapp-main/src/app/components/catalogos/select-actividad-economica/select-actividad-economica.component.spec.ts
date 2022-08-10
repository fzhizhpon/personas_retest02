import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VsSelectActividadEconomicaComponent } from './select-actividad-economica.component';

describe('SelectActividadEconomicaComponent', () => {
  let component: VsSelectActividadEconomicaComponent;
  let fixture: ComponentFixture<VsSelectActividadEconomicaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VsSelectActividadEconomicaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VsSelectActividadEconomicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
