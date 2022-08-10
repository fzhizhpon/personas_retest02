import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BienesMueblesComponent } from './bienes-muebles.component';

describe('BienesMueblesComponent', () => {
  let component: BienesMueblesComponent;
  let fixture: ComponentFixture<BienesMueblesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BienesMueblesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BienesMueblesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
