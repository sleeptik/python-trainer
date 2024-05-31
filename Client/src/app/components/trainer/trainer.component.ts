import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";
import {PythonService} from "../../services/python.service";
import {first, pipe, switchMap, tap} from "rxjs";
import {AssignmentsService} from "../../services/assignments.service";
import {AssignmentDetailsDto} from "../../models/assignment-details-dto";

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html'
})
export class TrainerComponent {
  assignment!: AssignmentDetailsDto;
  solution: string = "";
  output: string[] = [];


  constructor(
    activatedRoute: ActivatedRoute,
    private readonly pythonService: PythonService,
    private readonly assignmentsService: AssignmentsService
  ) {
    this.assignment = activatedRoute.snapshot.data["assignment"];
    this.solution = this.assignment.solution?.code ?? "";
  }

  get suggestions() {
    return this.assignment.suggestions;
  }

  executeSolution() {
    this.pythonService.getObservable()
      .pipe(first())
      .subscribe(value => this.output = value);

    this.pythonService.executeCode(this.solution);
  }

  verifySolution() {
    this.assignmentsService.setAssignmentSolution(this.assignment.id, this.solution).pipe(this.refresh()).subscribe()
  }

  clearOutput() {
    this.output = [];
  }

  setCode(code: string) {
    this.solution = code;
  }

  private refresh() {
    return pipe(
      switchMap(_ => this.assignmentsService.getAssignmentDetails(this.assignment.id)),
      tap(value => this.assignment = value)
    );
  }
}
