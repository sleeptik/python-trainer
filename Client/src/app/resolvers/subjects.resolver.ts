import {inject} from "@angular/core";
import {ResolveFn} from '@angular/router';
import {Subject} from "../models/subject";
import {SubjectsService} from "../services/subjects.service";

export const subjectsResolver: ResolveFn<Subject[]> = (route, state) => {
  const subjectsService = inject(SubjectsService);
  return subjectsService.getSubjects();
};

