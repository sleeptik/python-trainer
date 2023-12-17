import {Component, Input} from '@angular/core';
import {VerificationResult} from "../../models/verification-result";

@Component({
  selector: 'app-assignment-result',
  templateUrl: './assignment-result.component.html'
})
export class AssignmentResultComponent {
  @Input({required: true}) result: VerificationResult | undefined;
}
