import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { newExerciseResolver } from './new-exercise.resolver';

describe('newExerciseResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => newExerciseResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
