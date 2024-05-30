import {Exercise} from "./exercise";
import {Solution} from "./solution";
import {AssignmentStatus} from "./assignment-status";

export interface Assignment {
  id: number;
  studentId: number;

  exerciseId: number;
  exercise: Exercise;

  assignmentStatusId: number;
  assignmentStatus: AssignmentStatus;

  solutions: Solution[];

  assignedAt: Date;
}
