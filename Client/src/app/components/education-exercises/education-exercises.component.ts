import {Component, Input} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";

import {Assignment} from "../../models/assignment";

@Component({
  selector: 'app-education-exercises',
  templateUrl: './education-exercises.component.html'
})
export class EducationExercisesComponent {
  readonly displayedColumns = ["assignedAt", "exercise", "isFinished", "isPassed", "actions"];
  readonly dataSource: MatTableDataSource<Assignment> = new MatTableDataSource<Assignment>();

  @Input({required: true}) set assignments(value: Assignment[]) {
    this.dataSource.connect().next(value);
  };
}
