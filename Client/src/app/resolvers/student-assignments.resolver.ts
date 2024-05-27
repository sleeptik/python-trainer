import {inject} from "@angular/core";
import {ResolveFn} from '@angular/router';
import {AssignmentsService} from "../services/assignments.service";
import {Assignment} from "../models/assignment";

export const studentAssignmentsResolver: ResolveFn<Assignment[]> = (route, state) => {
  const educationService = inject(AssignmentsService);
  return educationService.getStudentAssignments();
};

