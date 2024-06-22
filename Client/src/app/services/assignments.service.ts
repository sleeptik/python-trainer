import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Assignment} from "../models/assignment";
import {SelfAssignmentRequest} from "../models/self-assignment-request";
import {SetAssignmentSolutionRequest} from "../models/set-assignment-solution-request";
import {AssignmentDetailsDto} from "../models/assignment-details-dto";


@Injectable({
  providedIn: "root"
})
export class AssignmentsService {
  constructor(private readonly httpClient: HttpClient) {
  }
  //Сервис для обращения к контроллеру назначенныз заданий на беке

  getStudentAssignments() {
    return this.httpClient.get<Assignment[]>("api/education/assignments");
  }

  getAssignmentDetails(assignmentId: number) {
    return this.httpClient.get<AssignmentDetailsDto>(`api/education/assignments/${assignmentId}`);
  }

  assignYourself(subjectId: number) {
    const request: SelfAssignmentRequest = {subjectId: subjectId};
    return this.httpClient.post<unknown>("api/education/assignments", request);
  }

  setAssignmentSolution(assignmentId: number, solution: string) {
    const request: SetAssignmentSolutionRequest = {solution: solution};
    return this.httpClient.post<unknown>(`api/education/assignments/${assignmentId}/solutions`, request);
  }
}

