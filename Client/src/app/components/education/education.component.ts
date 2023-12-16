import {Component} from '@angular/core';

import {ListAssignment} from "../../models/list-assignment";


const example: ListAssignment[] = [
  {exerciseId: 1, shortContents: "short contents", isFinished: true, isPassed: true, assignedAt: new Date()},
  {exerciseId: 2, shortContents: "short contents", isFinished: true, isPassed: false, assignedAt: new Date()},
  {exerciseId: 3, shortContents: "short contents", isFinished: true, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 4, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 5, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 6, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 7, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 8, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 9, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 10, shortContents: "short contents", isFinished: true, isPassed: true, assignedAt: new Date()},
  {exerciseId: 11, shortContents: "short contents", isFinished: true, isPassed: false, assignedAt: new Date()},
  {exerciseId: 12, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
  {exerciseId: 13, shortContents: "short contents", isFinished: false, isPassed: undefined, assignedAt: new Date()},
];

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent {
  protected readonly example = example;


  requestNewExercise($event: number | null) {
    
  }
}
