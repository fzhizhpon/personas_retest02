import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferenciaFinancieraComponent } from './referencia-financiera.component';

describe('ReferenciaFinancieraComponent', () => {
  let component: ReferenciaFinancieraComponent;
  let fixture: ComponentFixture<ReferenciaFinancieraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReferenciaFinancieraComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferenciaFinancieraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
