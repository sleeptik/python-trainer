import {ResolveFn} from '@angular/router';
import {inject} from "@angular/core";
import {EducationService} from "../services/education.service";
import {Subject} from "../models/subject";

export const mySubjectsResolver: ResolveFn<Subject[]> = (route, state) => {
  const educationService = inject(EducationService);
  return educationService.getMySubjects();
};

