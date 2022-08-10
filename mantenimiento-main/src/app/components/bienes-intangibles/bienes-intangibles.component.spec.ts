import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BienesIntangiblesComponent } from './bienes-intangibles.component';

describe('BienesIntangiblesComponent', () => {
  let component: BienesIntangiblesComponent;
  let fixture: ComponentFixture<BienesIntangiblesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BienesIntangiblesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BienesIntangiblesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
