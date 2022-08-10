/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FakerService } from './faker.service';

describe('Service: Faker', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FakerService]
    });
  });

  it('should ...', inject([FakerService], (service: FakerService) => {
    expect(service).toBeTruthy();
  }));
});
