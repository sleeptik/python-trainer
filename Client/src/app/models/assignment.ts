import {Exercise} from "./exercise";
import { Solution } from "./solution";

export interface Assignment {
  id: number;
  studentId: number;

  exerciseId: number;
  exercise: Exercise;

  solutions: Solution[];

  assignedAt: Date;
}
