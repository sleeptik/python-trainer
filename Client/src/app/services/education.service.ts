import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Exercise} from "../models/exercise";

import {FinishExerciseRequest} from "../models/finish-exercise.request";

@Injectable({
  providedIn: "root"
})
export class EducationService {
  constructor(private readonly httpClient: HttpClient) {
  }

  newExercise(subjectId: number = 3) {
    const params = new HttpParams().set("subjectId", subjectId);
    return this.httpClient.get<Exercise>("api/education/new", {params: params});
  }

  finishExercise(studentId: number, exerciseId: number, solution: string) {
    const request: FinishExerciseRequest = {studentId: studentId, exerciseId: exerciseId};
    return this.httpClient.patch<void>("api/education/finish", request);
  }
}
