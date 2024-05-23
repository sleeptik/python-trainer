import {ResolveFn} from '@angular/router';
import {Assignment} from "../models/assignment";
import {inject} from "@angular/core";
import {EducationService} from "../services/education.service";

export const assignmentResolver: ResolveFn<Assignment> = (route, state) => {
  const educationService = inject(EducationService);
  const assignmentId = parseInt(route.paramMap.get("assignmentId")!);
  return educationService.getAssignment(assignmentId);
};

