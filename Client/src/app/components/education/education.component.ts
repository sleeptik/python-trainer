import {Component} from '@angular/core';

import {EducationService} from "../../services/education.service";
import {Assignment} from "../../models/assignment";
import {Subject} from "../../models/subject";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent {
  assignments: Assignment[] = [];
  subjects: Subject[] = [];

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly educationService: EducationService
  ) {
    const data = this.activatedRoute.snapshot.data;
    this.assignments = data["assignments"];
    this.subjects = data["subjects"];
  }

  requestNewExercise(subjectId: number | null) {
    this.educationService.selfAssignNewExercise(subjectId).subscribe(
      value => this.assignments.unshift(value)
    );
  }
}
