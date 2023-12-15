import {Component, OnInit} from '@angular/core';
import {ExerciseResultComponent} from "../exercise-result/exercise-result.component";
import {Exercise} from "../../models/exercise";
import {VerificationResult} from "../../models/verification-result";
import {ActivatedRoute} from "@angular/router";
import {EducationService} from "../../services/education.service";

@Component({
  selector: 'app-exercise',
  standalone: true,
  imports: [
    ExerciseResultComponent
  ],
  templateUrl: './exercise.component.html'
})
export class ExerciseComponent implements OnInit {
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
    this.educationService.finishExercise(1, this.exercise.id, solution).subscribe(value => this.result = value);
  }
}
