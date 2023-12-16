import {TestBed} from '@angular/core/testing';
import {ResolveFn} from '@angular/router';

import {exerciseResolver} from './exercise-resolver';
import {Exercise} from "../models/exercise";

describe('exerciseResolver', () => {
  const executeResolver: ResolveFn<Exercise> = (...resolverParameters) =>
    TestBed.runInInjectionContext(() => exerciseResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
