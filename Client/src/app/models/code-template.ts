import {Exercise} from "./exercise";
import {Solution} from "./solution";
import {AssignmentStatus} from "./assignment-status";

export interface CodeTemplate {
  id: number;
  skippedPartName: string;
  code: string;

  exerciseId: number;
  exercise: Exercise;
}
