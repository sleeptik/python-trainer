import {ResolveFn} from '@angular/router';
import {inject} from "@angular/core";
import {EducationService} from "../services/education.service";
import {Exercise} from "../models/exercise";

export const newExerciseResolver: ResolveFn<Exercise> = (route, state) => {
  const educationService = inject(EducationService);
  return educationService.newExercise();
};
