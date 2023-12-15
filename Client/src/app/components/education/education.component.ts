import {Component, OnInit} from '@angular/core';
import {EducationService} from "../../services/education.service";
import {Exercise} from "../../models/exercise";
import {ActivatedRoute} from "@angular/router";
import {VerificationResult} from "../exercise-result/exercise-result.component";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent implements OnInit {
  exercise!: Exercise;
  result: VerificationResult | undefined;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly educationService: EducationService,
  ) {
  }

  ngOnInit(): void {
    this.exercise = this.activatedRoute.snapshot.data["exercise"];
  }

  sendSolution(solution: string) {
    this.educationService.finishExercise(1, this.exercise.id, solution).subscribe();
  }
}
