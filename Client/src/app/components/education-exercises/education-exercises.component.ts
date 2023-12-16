import {Component, Input} from '@angular/core';
import {MatTableDataSource, MatTableModule} from "@angular/material/table";

import {ListAssignment} from "../../models/list-assignment";

@Component({
  selector: 'app-education-exercises',
  templateUrl: './education-exercises.component.html'
})
export class EducationExercisesComponent {
  readonly displayedColumns = ["assignedAt", "exercise", "isFinished", "isPassed", "actions"];
  dataSource!: MatTableDataSource<ListAssignment>;

  @Input({required: true}) set assignments(value: ListAssignment[]) {
    this.dataSource = new MatTableDataSource<ListAssignment>(value);
  };
}
