import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Assignment} from "../models/assignment";
import {SelfAssignmentRequest} from "../models/self-assignment-request";
import {SetAssignmentSolutionRequest} from "../models/set-assignment-solution-request";


@Injectable({
  providedIn: "root"
})
export class AssignmentsService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getStudentAssignments() {
    return this.httpClient.get<Assignment[]>("api/education/assignments");
  }

  getAssignmentDetails(assignmentId: number) {
    return this.httpClient.get<Assignment>(`api/education/assignments/${assignmentId}`);
  }

  assignYourself(subjectId: number) {
    const request: SelfAssignmentRequest = {subjectId: subjectId};
    return this.httpClient.post<Assignment>("api/education/assignments", request);
  }

  setAssignmentSolution(assignmentId: number, solution: string) {
    const request: SetAssignmentSolutionRequest = {solution: solution};
    return this.httpClient.patch<unknown>(`api/education/assignments/${assignmentId}/solutions`, request);
  }
}

