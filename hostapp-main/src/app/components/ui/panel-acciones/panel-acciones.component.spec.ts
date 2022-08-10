import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PanelAccionesComponent } from './panel-acciones.component';

describe('PanelAccionesComponent', () => {
  let component: PanelAccionesComponent;
  let fixture: ComponentFixture<PanelAccionesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PanelAccionesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PanelAccionesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
