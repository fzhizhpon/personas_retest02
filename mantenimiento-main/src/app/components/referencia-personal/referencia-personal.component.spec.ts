import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferenciaPersonalComponent } from './referencia-personal.component';

describe('ReferenciaPersonalComponent', () => {
  let component: ReferenciaPersonalComponent;
  let fixture: ComponentFixture<ReferenciaPersonalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReferenciaPersonalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferenciaPersonalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
