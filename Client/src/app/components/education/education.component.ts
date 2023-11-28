import {Component, OnInit} from '@angular/core';
import {EducationService} from "../../services/education.service";
import {Exercise} from "../../models/exercise";
import {ExerciseHistory} from "./exercise-history";
import {EducationAdminService} from "../../services/education-admin.service";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent implements OnInit {
  exercise!: Exercise;
  history: ExerciseHistory | undefined;

  constructor(
    private readonly educationService: EducationService,
    private readonly educationAdminService: EducationAdminService
  ) {
  }

  ngOnInit(): void {
    this.extracted();
  }

  private extracted() {
    this.educationService.getNewExercise().subscribe(
      value => this.exercise = value
    );
  }

  sendSolution(solution: string) {
    this.history = {exerciseId: this.exercise.id, userId: 1};
  }

  finishedSuccessfully() {
    this.educationAdminService.setStatus(1, this.exercise.id, true).subscribe(
      () => {
        this.history = undefined;
        this.extracted();
      }
    );
  }

  finishedWithErrors() {
    this.educationAdminService.setStatus(1, this.exercise.id, false).subscribe(
      () => {
        this.history = undefined;
        this.extracted();
      }
    );
  }
}
