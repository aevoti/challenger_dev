/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TopicoService } from './topico.service';

describe('Service: Topico', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TopicoService]
    });
  });

  it('should ...', inject([TopicoService], (service: TopicoService) => {
    expect(service).toBeTruthy();
  }));
});
