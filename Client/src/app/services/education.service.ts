import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Assignment} from "../models/assignment";
import {SelfAssignmentRequest} from "../models/self-assignment-request";
import {SetAssignmentSolutionRequest} from "../models/set-assignment-solution-request";
import {Subject} from "../models/subject";


@Injectable({
  providedIn: "root"
})
export class EducationService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getMyAssignments() {
    return this.httpClient.get<Assignment[]>("api/education");
  }

  getAssignment(exerciseId: number) {
    return this.httpClient.get<Assignment>(`api/education/exercises/${exerciseId}`);
  }

  selfAssignNewExercise(subjectId: number) {
    const request: SelfAssignmentRequest = {subjectId: subjectId};
    return this.httpClient.post<Assignment>("api/education", request);
  }

  setAssignmentSolution(exerciseId: number, solution: string) {
    const request: SetAssignmentSolutionRequest = {exerciseId: exerciseId, studentId: 1, solution: solution};
    return this.httpClient.patch<never>("api/education", request);
  }

  getMySubjects() {
    return this.httpClient.get<Subject[]>("api/education/subjects");
  }
}

