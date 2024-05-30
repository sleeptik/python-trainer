import {inject} from "@angular/core";
import {ResolveFn} from '@angular/router';
import {AssignmentsService} from "../services/assignments.service";
import {AssignmentDetailsDto} from "../models/assignment-details-dto";

export const assignmentDetailsResolver: ResolveFn<AssignmentDetailsDto> = (route, state) => {
  const educationService = inject(AssignmentsService);
  const assignmentId = parseInt(route.paramMap.get("assignmentId")!);
  return educationService.getAssignmentDetails(assignmentId);
};

