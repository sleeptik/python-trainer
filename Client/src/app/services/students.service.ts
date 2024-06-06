import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Student} from "../models/student";
import {of, tap} from "rxjs";

@Injectable({
  providedIn: "root"
})
export class StudentsService {
  student: Student | null;

  constructor(private readonly httpClient: HttpClient) {
    this.student = null;
  }

  getStudentMe() {
    if (this.student)
      return of(this.student)
    return this.httpClient.get<Student>("api/education/students/me").pipe(
      tap(value => this.student = value)
    )
  }

}

