import { TestBed } from '@angular/core/testing';

import { ValidacionTelefonosService } from './validacion-telefonos.service';

describe('ValidacionTelefonosService', () => {
  let service: ValidacionTelefonosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ValidacionTelefonosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
