import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";
import {Subject} from "../../models/subject";

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html'
})
export class AssignmentsComponent {
  assignments: Assignment[] = [];
  subjects: Subject[];

  constructor(activatedRoute: ActivatedRoute) {
    this.assignments = activatedRoute.snapshot.data["assignments"];
    this.subjects = activatedRoute.snapshot.data["subjects"];
  }
}
