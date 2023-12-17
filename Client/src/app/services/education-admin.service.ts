import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UnverifiedAssignmentResponse} from "../models/unverified-assignment.response";
import {of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class EducationAdminService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getUnverifiedAssignments() {
    return this.httpClient.get<UnverifiedAssignmentResponse[]>('api/admin/education/unverified');
  }

  setStatus(studentId: number, exerciseId: number, status: boolean) {
    // const request: SetAssignmentResultRequest = {studentId: studentId, exerciseId: exerciseId, status: status};
    // return this.httpClient.patch<void>('api/admin/education/status', request);
    return of();
  }
}
