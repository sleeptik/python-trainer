import {Component, OnInit} from '@angular/core';
import {EducationService} from "../../services/education.service";
import {Exercise} from "../../models/exercise";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent implements OnInit {
  exercise!: Exercise;

  constructor(
    private readonly educationService: EducationService) {
  }

  ngOnInit(): void {
    this.educationService.getNewExercise().subscribe(
      value => this.exercise = value
    );
  }

  sendSolution(solution: string) {
    this.educationService
  }
}
