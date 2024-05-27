import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";
import {Subject} from "../../models/subject";
import {AssignmentsService} from "../../services/assignments.service";

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html'
})
export class AssignmentsComponent {
  assignments: Assignment[] = [];
  subjects: Subject[];

  constructor(activatedRoute: ActivatedRoute,
              private readonly educationService: AssignmentsService) {
    this.assignments = activatedRoute.snapshot.data["assignments"];
    this.subjects = activatedRoute.snapshot.data["subjects"];
  }

  assignRandomSubjectExercise() {
    const index = Math.round(Math.random() * (this.subjects.length - 1));
    const subjectId = this.subjects[index].id;
    this.educationService.assignYourself(subjectId).subscribe();
  }

  assignSelectedSubjectExercise(subjectId: number) {
    this.educationService.assignYourself(subjectId).subscribe();
  }
}
