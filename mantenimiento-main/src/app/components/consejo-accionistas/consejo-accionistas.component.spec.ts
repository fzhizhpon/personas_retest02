import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsejoAccionistasComponent } from './consejo-accionistas.component';

describe('ConsejoAccionistasComponent', () => {
  let component: ConsejoAccionistasComponent;
  let fixture: ComponentFixture<ConsejoAccionistasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsejoAccionistasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsejoAccionistasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
