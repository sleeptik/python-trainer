import {Component, Input} from '@angular/core';
import {ListAssignment} from "../../models/list-assignment";

@Component({
  selector: 'app-education-history',
  templateUrl: './education-history.component.html'
})
export class EducationHistoryComponent {
  @Input({required: true, transform: historyTransform}) assignments!: ListAssignment[];
}

function historyTransform(assignments: ListAssignment[]) {
  return assignments.filter(value => value.isFinished)
    .toReversed()
    .slice(0, 12);
}
