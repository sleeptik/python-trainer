import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";
import {Subject} from "../../models/subject";
import {EducationService} from "../../services/education.service";

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html'
})
export class AssignmentsComponent {
  assignments: Assignment[] = [];
  subjects: Subject[];

  constructor(activatedRoute: ActivatedRoute,
              private readonly educationService: EducationService) {
    this.assignments = activatedRoute.snapshot.data["assignments"];
    this.subjects = activatedRoute.snapshot.data["subjects"];
  }

  assignRandomSubjectExercise() {
    const subjectId = Math.round(Math.random() * (this.subjects.length - 1));
    this.educationService.selfAssignNewExercise(subjectId).subscribe();
  }

  assignSelectedSubjectExercise(subjectId: number) {
    this.educationService.selfAssignNewExercise(subjectId).subscribe();
  }
}
