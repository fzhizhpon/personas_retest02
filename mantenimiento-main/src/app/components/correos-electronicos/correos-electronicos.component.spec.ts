import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CorreosElectronicosComponent } from './correos-electronicos.component';

describe('CorreosElectronicosComponent', () => {
  let component: CorreosElectronicosComponent;
  let fixture: ComponentFixture<CorreosElectronicosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CorreosElectronicosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CorreosElectronicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
