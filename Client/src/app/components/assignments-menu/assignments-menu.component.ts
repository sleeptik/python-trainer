import {Component, Input} from '@angular/core';
import {Subject} from "../../models/subject";
import {Assignment} from "../../models/assignment";

@Component({
  selector: 'app-assignments-menu',
  templateUrl: './assignments-menu.component.html'
})
export class AssignmentsMenuComponent {
  @Input({required: true}) subjects!: Subject[];
  @Input({required: true}) assignments!: Assignment[];

  get total(): number {
    return this.assignments.length;
  }

  get totalFinished(): number {
    return this.assignments.reduce(
      (previousValue, currentValue) => previousValue + (currentValue.finishedAt ? 1 : 0), 0
    );
  }

  get totalSuccessfullyCompleted(): number {
    return this.assignments.reduce(
      (previousValue, currentValue) => previousValue + (currentValue.isPassed === true ? 1 : 0), 0
    );
  }
}
