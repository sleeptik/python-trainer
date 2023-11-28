import {Component, EventEmitter, Output} from '@angular/core';
import {EducationAdminService} from "../../services/education-admin.service";

@Component({
  selector: 'app-verifier',
  templateUrl: './verifier.component.html'
})
export class VerifierComponent {
  @Output() good = new EventEmitter<unknown>();
  @Output() bad = new EventEmitter<unknown>();
  history: any | undefined;


  constructor(private readonly educationAdminService: EducationAdminService) {
  }
}
