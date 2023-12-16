export interface ListAssignment {
  exerciseId: number;
  shortContents: string;
  isFinished: boolean;
  isPassed: boolean | undefined;
  assignedAt: Date;
}
