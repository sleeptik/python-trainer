import {Component, OnInit} from '@angular/core';
import {EducationService} from "../../services/education.service";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent implements OnInit {
  exercises: any[] = [];

  constructor(private readonly educationService: EducationService) {
  }

  ngOnInit(): void {
    this.educationService.getNewExercises().subscribe((value: any) => this.exercises = value);
  }
}
