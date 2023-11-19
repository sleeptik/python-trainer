import {Component, Input} from '@angular/core';
import {Exercise} from "../../models/exercise";

@Component({
  selector: 'app-education-exercise',
  templateUrl: './education-exercise.component.html'
})
export class EducationExerciseComponent {
  @Input({required: true}) exercise!: Exercise;
}
