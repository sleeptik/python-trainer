import {Component, EventEmitter, Input, Output} from '@angular/core';
import {EducationAdminService} from "../../services/education-admin.service";
import {Assignment} from "../education/assignment";

@Component({
  selector: 'app-verifier',
  templateUrl: './verifier.component.html'
})
export class VerifierComponent {
  @Output() good = new EventEmitter<unknown>();
  @Output() bad = new EventEmitter<unknown>();
  @Input() assignments: Assignment[] = [];

  constructor(private readonly educationAdminService: EducationAdminService) {
  }
}
