import {ResolveFn} from '@angular/router';
import {Exercise} from "../models/exercise";
import {of} from "rxjs";

export const exerciseResolver: ResolveFn<Exercise> = (route, state) => {
  const exerciseId = parseInt(route.paramMap.get("exerciseId")!)
  const exercise: Exercise = {id: 1, rank: "RANK MOCK", contents: "CONTENT MOCK", subjects: ["SUBJECT MOCK"]};
  return of(exercise);
};
