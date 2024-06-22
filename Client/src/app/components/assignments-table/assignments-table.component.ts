import {Component, Input} from '@angular/core';
import {CdkTableDataSourceInput} from "@angular/cdk/table";
import {Assignment} from "../../models/assignment";

@Component({
  selector: 'app-assignments-table',
  templateUrl: './assignments-table.component.html',
})
export class AssignmentsTableComponent {
  readonly columns: string[] = ["date", "description", "status", "actions"] as const;
  dataSource: CdkTableDataSourceInput<Assignment> = [];
  //Компонент таблицы назначенных заданий, с сортировкой по дате назначения
  @Input({required: true}) set assignments(assignments: Assignment[]) {
    this.dataSource = assignments.sort((a, b) => b.assignedAt > a.assignedAt ? 1 : b.assignedAt === a.assignedAt ? 0 : -1);
  }
}
