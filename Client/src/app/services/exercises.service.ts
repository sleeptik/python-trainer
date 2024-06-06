import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CodeTemplate} from "../models/code-template";

@Injectable({
  providedIn: "root"
})
export class ExercisesService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getCodeTemplates(exerciseId:number) {
    return this.httpClient.get<CodeTemplate[]>(`api/education/exercises/${exerciseId}/code-template`);
  }

}

