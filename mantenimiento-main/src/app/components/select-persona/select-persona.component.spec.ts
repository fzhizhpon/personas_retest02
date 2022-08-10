import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VsSelectPersonaComponent } from './select-persona.component';

describe('SelectPersonaComponent', () => {
  let component: VsSelectPersonaComponent;
  let fixture: ComponentFixture<VsSelectPersonaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VsSelectPersonaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VsSelectPersonaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
