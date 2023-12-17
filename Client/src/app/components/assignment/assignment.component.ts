import {Component, OnInit} from '@angular/core';
import {VerificationResult} from "../../models/verification-result";
import {ActivatedRoute} from "@angular/router";
import {EducationService} from "../../services/education.service";
import {Assignment} from "../../models/assignment";

@Component({
  selector: 'app-assignment',
  templateUrl: './assignment.component.html'
})
export class AssignmentComponent implements OnInit {
  assignment!: Assignment;
  result: VerificationResult | undefined;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly educationService: EducationService,
  ) {
  }

  ngOnInit(): void {
    this.assignment = this.activatedRoute.snapshot.data["exercise"];
  }

  sendSolution(solution: string) {
    this.educationService.setAssignmentSolution(this.assignment.exerciseId, solution).subscribe(
      value => this.result = value
    );
  }
}
