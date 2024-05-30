import {Exercise} from "./exercise";
import {Solution} from "./solution";
import {Suggestion} from "./suggestion";

export interface AssignmentDetailsDto {
  id: number;
  exercise: Exercise;
  solution: Solution | null;
  suggestions: Suggestion[] | null;
}
