import {Component} from '@angular/core';

import {EducationService} from "../../services/education.service";
import {Assignment} from "../../models/assignment";
import {Subject} from "../../models/subject";
import {ActivatedRoute} from "@angular/router";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html'
})
export class EducationComponent {
  assignments: BehaviorSubject<Assignment[]>;
  subjects: Subject[] = [];

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly educationService: EducationService
  ) {
    const data = this.activatedRoute.snapshot.data;
    this.assignments = new BehaviorSubject<Assignment[]>(data["assignments"]);
    this.subjects = data["subjects"];
  }

  requestNewExercise(subjectId: number) {
    this.educationService.selfAssignNewExercise(subjectId).subscribe(
      value => {
        let currentAssignments = this.assignments.value;
        let newAssignments = [value].concat(currentAssignments);
        this.assignments.next(newAssignments);
      }
    );
  }
}
