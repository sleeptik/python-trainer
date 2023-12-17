import {Component, Input} from '@angular/core';
import {Assignment} from "../../models/assignment";

@Component({
  selector: 'app-education-history-bar',
  templateUrl: './education-history-bar.component.html'
})
export class EducationHistoryBarComponent {
  @Input({required: true, transform: historyTransform}) assignments!: Assignment[];
}

function historyTransform(assignments: Assignment[]) {
  return assignments.filter(value => !!value.finishedAt)
    .toReversed()
    .slice(0, 12);
}
