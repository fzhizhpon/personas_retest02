import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelacionInstitucionComponent } from './relacion-institucion.component';

describe('RelacionInstitucionComponent', () => {
  let component: RelacionInstitucionComponent;
  let fixture: ComponentFixture<RelacionInstitucionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RelacionInstitucionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RelacionInstitucionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
