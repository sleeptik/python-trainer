import {Component, Input} from '@angular/core';
import {VerificationResult} from "../../models/verification-result";

@Component({
  selector: 'app-exercise-result',
  templateUrl: './exercise-result.component.html'
})
export class ExerciseResultComponent {
  @Input({required: true}) result: VerificationResult | undefined;
}
