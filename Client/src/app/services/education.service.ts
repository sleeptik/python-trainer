import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Exercise} from "../models/exercise";

@Injectable({
  providedIn: "root"
})
export class EducationService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getNewExercises() {
    return this.httpClient.get<Exercise>("api/education/new");
  }
}
