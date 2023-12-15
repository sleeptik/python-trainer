import {Component, Input} from '@angular/core';
import {NgIf} from "@angular/common";
import {VerificationResult} from "../../models/verification-result";

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
