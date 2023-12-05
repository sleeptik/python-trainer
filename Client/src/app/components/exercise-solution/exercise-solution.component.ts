import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-exercise-solution',
  templateUrl: './exercise-solution.component.html'
})
export class ExerciseSolutionComponent {
  @Output() readonly solve = new EventEmitter<string>();
  solution: string = "";
}
