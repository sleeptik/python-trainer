import {ResolveFn} from "@angular/router";
import {Assignment} from "../../models/assignment";

export const myAssignmentsMockResolver: ResolveFn<Assignment[]> = (route, state) => {
  return [];
};
