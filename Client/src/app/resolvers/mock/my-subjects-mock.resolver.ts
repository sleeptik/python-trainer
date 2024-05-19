import {ResolveFn} from "@angular/router";
import {Subject} from "../../models/subject";

export const mySubjectsMockResolver: ResolveFn<Subject[]> = (route, state) => {
  return [];
};
