import {Component, OnInit} from '@angular/core';
import {EducationService} from "../../services/education.service";
import {Exercise} from "../../models/exercise";
import {Assignment} from "./assignment";
import {EducationAdminService} from "../../services/education-admin.service";
import {switchMap} from "rxjs";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent implements OnInit {
  exercise!: Exercise;
  assignments: Assignment[] = [];

  constructor(
    private readonly educationService: EducationService,
    private readonly educationAdminService: EducationAdminService
  ) {
  }

  ngOnInit(): void {
    this.getNewExercise();
  }

  sendSolution(solution: string) {
    this.educationService.finishExercise(1, this.exercise.id, solution)
      .pipe(switchMap(this.educationAdminService.getUnverifiedAssignments))
      .subscribe(value => this.assignments = value);
  }

  finishedSuccessfully() {
    this.educationAdminService.setStatus(1, this.exercise.id, true).subscribe(
      () => this.getNewExercise()
    );
  }

  finishedWithErrors() {
    this.educationAdminService.setStatus(1, this.exercise.id, false).subscribe(
      () => this.getNewExercise()
    );
  }

  private getNewExercise() {
    this.educationService.newExercise().subscribe(
      value => this.exercise = value
    );
  }
}
