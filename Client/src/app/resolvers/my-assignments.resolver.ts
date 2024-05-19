import {ResolveFn} from '@angular/router';
import {inject} from "@angular/core";
import {EducationService} from "../services/education.service";
import {Assignment} from "../models/assignment";

export const myAssignmentsResolver: ResolveFn<Assignment[]> = (route, state) => {
  const educationService = inject(EducationService);
  return educationService.getMyAssignments();
};

