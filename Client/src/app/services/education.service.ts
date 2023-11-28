import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Exercise} from "../models/exercise";

@Injectable({
  providedIn: "root"
})
export class EducationService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getNewExercise() {
    const params = new HttpParams().set("subjectId", 3);
    return this.httpClient.get<Exercise>("api/education/new", {params: params});
  }
}
