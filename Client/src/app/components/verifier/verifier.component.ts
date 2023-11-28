import {Component, EventEmitter, Input, Output} from '@angular/core';
import {EducationAdminService} from "../../services/education-admin.service";
import {ExerciseHistory} from "../education/exercise-history";

@Component({
  selector: 'app-verifier',
  templateUrl: './verifier.component.html'
})
export class VerifierComponent {
  @Output() good = new EventEmitter<unknown>();
  @Output() bad = new EventEmitter<unknown>();
  @Input() history: ExerciseHistory | undefined;


  constructor(private readonly educationAdminService: EducationAdminService) {
  }
}
