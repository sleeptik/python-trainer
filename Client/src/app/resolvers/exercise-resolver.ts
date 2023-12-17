import {ResolveFn} from '@angular/router';
import {inject} from "@angular/core";
import {EducationService} from "../services/education.service";
import {Assignment} from "../models/assignment";

export const exerciseResolver: ResolveFn<Assignment> = (route, state) => {
  const exerciseId = parseInt(route.paramMap.get("exerciseId")!);
  // const exercise: Exercise = {
  //   id: 1,
  //   rank: {id: 1, name: "RANK MOCK"},
  //   contents: "CONTENT MOCK",
  //   subjects: [{id: 1, name: "SUBJECT MOCK"}]
  // };
  //
  const educationService = inject(EducationService);
  return educationService.getAssignment(exerciseId);
};
