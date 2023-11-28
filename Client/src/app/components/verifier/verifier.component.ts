import {Component} from '@angular/core';
import {EducationAdminService} from "../../services/education-admin.service";

@Component({
  selector: 'app-verifier',
  templateUrl: './verifier.component.html'
})
export class VerifierComponent {
  history: any | undefined;

  constructor(private readonly educationAdminService: EducationAdminService) {
  }
}
