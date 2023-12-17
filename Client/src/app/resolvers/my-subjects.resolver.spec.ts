import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { mySubjectsResolver } from './my-subjects.resolver';

describe('mySubjectsResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => mySubjectsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
