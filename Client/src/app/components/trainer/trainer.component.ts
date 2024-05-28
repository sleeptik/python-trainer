import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";
import {PythonService} from "../../services/python.service";
import {first} from "rxjs";
import {AssignmentsService} from "../../services/assignments.service";

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html'
})
export class TrainerComponent {
  readonly assignment!: Assignment;
  solution: string = "";
  output: string[] = [];


  constructor(
    activatedRoute: ActivatedRoute,
    private readonly pythonService: PythonService,
    private readonly assignmentsService: AssignmentsService
  ) {
    this.assignment = activatedRoute.snapshot.data["assignment"];
  }

  executeSolution() {
    this.pythonService.getObservable()
      .pipe(first())
      .subscribe(value => this.output = value);

    this.pythonService.executeCode(this.solution);
  }

  verifySolution() {
    this.assignmentsService.setAssignmentSolution(this.assignment.id, this.solution).subscribe()
  }

  clearOutput() {
    this.output = [];
  }

  setCode(code: string) {
    this.solution = code;
  }
}
