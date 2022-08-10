import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DigitalizacionDocumentosComponent } from './digitalizacion-documentos.component';

describe('DigitalizacionDocumentosComponent', () => {
  let component: DigitalizacionDocumentosComponent;
  let fixture: ComponentFixture<DigitalizacionDocumentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DigitalizacionDocumentosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DigitalizacionDocumentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
