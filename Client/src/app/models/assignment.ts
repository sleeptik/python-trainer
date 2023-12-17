import {Exercise} from "./exercise";

export interface Assignment {
  studentId: number;

  exerciseId: number;
  exercise: Exercise;

  solution: string | null;
  isPassed: boolean | null;

  assignedAt: Date;
  finishedAt: Date | null;
}
