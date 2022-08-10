import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonaDocComponent } from './persona-doc.component';

describe('PersonaDocComponent', () => {
  let component: PersonaDocComponent;
  let fixture: ComponentFixture<PersonaDocComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonaDocComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonaDocComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
