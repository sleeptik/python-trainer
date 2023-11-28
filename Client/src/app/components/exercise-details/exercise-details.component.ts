import {Component, Input} from '@angular/core';
import {Exercise} from "../../models/exercise";

@Component({
  selector: 'app-exercise-details',
  templateUrl: './exercise-details.component.html'
})
export class ExerciseDetailsComponent {
  @Input({required: true}) exercise!: Exercise;
}
