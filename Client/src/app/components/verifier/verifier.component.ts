import {Component, Input, OnInit} from '@angular/core';
import {EducationAdminService} from "../../services/education-admin.service";
import {Assignment} from "../../models/assignment";

@Component({
  selector: 'app-verifier',
  templateUrl: './verifier.component.html'
})
export class VerifierComponent implements OnInit {
  @Input() assignments: Assignment[] = [];

  constructor(private readonly educationAdminService: EducationAdminService) {
  }

  ngOnInit(): void {
    this.educationAdminService.getUnverifiedAssignments().subscribe(
      value => this.assignments = value
    );
  }

  setFinished(assignment: Assignment) {
    this.educationAdminService.setStatus(assignment.studentId, assignment.exerciseId, true).subscribe(value => {
      const index = this.assignments.indexOf(assignment);
      this.assignments.splice(index, 1);
    });
  }

  setFailed(assignment: Assignment) {
    this.educationAdminService.setStatus(assignment.studentId, assignment.exerciseId, false).subscribe(value => {
      const index = this.assignments.indexOf(assignment);
      this.assignments.splice(index, 1);
    });
  }
}
