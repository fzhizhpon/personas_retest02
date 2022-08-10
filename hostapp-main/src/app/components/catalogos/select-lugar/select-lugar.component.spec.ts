import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectLugarComponent } from './select-lugar.component';

describe('SelectLugarComponent', () => {
  let component: SelectLugarComponent;
  let fixture: ComponentFixture<SelectLugarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectLugarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectLugarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
