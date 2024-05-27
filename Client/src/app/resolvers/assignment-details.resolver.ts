import {inject} from "@angular/core";
import {ResolveFn} from '@angular/router';
import {Assignment} from "../models/assignment";
import {AssignmentsService} from "../services/assignments.service";

export const assignmentDetailsResolver: ResolveFn<Assignment> = (route, state) => {
  const educationService = inject(AssignmentsService);
  const assignmentId = parseInt(route.paramMap.get("assignmentId")!);
  return educationService.getAssignmentDetails(assignmentId);
};

