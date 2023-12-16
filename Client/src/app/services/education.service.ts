import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ListAssignment} from "../models/list-assignment";


@Injectable({
  providedIn: "root"
})
export class EducationService {
  constructor(private readonly httpClient: HttpClient) {
  }

  myExercises() {
    return this.httpClient.get<ListAssignment[]>("api/education");
  }

  newExercise(subjectId: number | null) {
    const request = {subjectId: subjectId};
    return this.httpClient.post<ListAssignment>("", request);
  }
}
