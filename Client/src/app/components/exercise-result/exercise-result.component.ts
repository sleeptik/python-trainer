import {Component, Input} from '@angular/core';
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-exercise-result',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './exercise-result.component.html'
})
export class ExerciseResultComponent {
  @Input({required: true}) result: VerificationResult | undefined;
}

export interface VerificationResult {
  valid: boolean;
  errors: Array<string> | undefined;
  suggestions: Array<string> | undefined;
}
