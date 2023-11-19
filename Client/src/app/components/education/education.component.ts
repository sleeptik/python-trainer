import {Component, OnInit} from '@angular/core';
import {EducationService} from "../../services/education.service";
import {Exercise} from "../../models/exercise";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent implements OnInit {
  currentExercise!: Exercise;
  exercises: Exercise[] = [];

  constructor(private readonly educationService: EducationService) {
  }

  ngOnInit(): void {
    this.educationService.getNewExercises().subscribe((value: any) => {
      this.exercises = value;
      this.currentExercise = this.exercises[0];
    });
  }

  changeExercise(number: number) {
    this.currentExercise = this.exercises[number];
  }
}
