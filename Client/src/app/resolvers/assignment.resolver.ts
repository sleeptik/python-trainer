import {ResolveFn} from '@angular/router';
import {Assignment} from "../models/assignment";

export const assignmentResolver: ResolveFn<Assignment> = (route, state) => {
  const assignment: Assignment = {
    exerciseId: 1,
    studentId: 1,
    exercise: {
      id: 1,
      rank: {id: 1, name: "RANK MOCK"},
      contents: "CONTENT MOCK",
      subjects: [{id: 1, name: "SUBJECT MOCK"}]
    },
    assignedAt: new Date(),
    finishedAt: new Date(),
    isPassed: true,
    solution: "Solution"
  };
  return assignment;
};
