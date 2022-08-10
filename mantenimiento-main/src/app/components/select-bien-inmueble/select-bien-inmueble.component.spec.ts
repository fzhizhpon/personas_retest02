import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VsSelectBienInmuebleComponent } from './select-bien-inmueble.component';

describe('SelectBienInmuebleComponent', () => {
  let component: VsSelectBienInmuebleComponent;
  let fixture: ComponentFixture<VsSelectBienInmuebleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VsSelectBienInmuebleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VsSelectBienInmuebleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
