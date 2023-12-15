import {ResolveFn} from '@angular/router';
import {Exercise} from "../models/exercise";
import {of} from "rxjs";

export const newExerciseResolver: ResolveFn<Exercise> = (route, state) => {
  // const educationService = inject(EducationService);
  // return educationService.newExercise();

  const exercise: Exercise = {id: 1, rank: "RANK MOCK", contents: "CONTENT MOCK", subjects: ["SUBJECT MOCK"]};
  return of(exercise);
};
