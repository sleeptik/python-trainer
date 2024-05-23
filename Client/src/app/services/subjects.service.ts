import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Subject} from "../models/subject";

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getSubjects() {
    return this.httpClient.get<Subject[]>("api/subjects");
  }
}
