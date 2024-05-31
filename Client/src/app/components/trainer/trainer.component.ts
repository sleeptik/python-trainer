import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";
import {PythonService} from "../../services/python.service";
import {first} from "rxjs";
import {AssignmentsService} from "../../services/assignments.service";
import {AssignmentDetailsDto} from "../../models/assignment-details-dto";

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html'
})
export class TrainerComponent {
  readonly assignment!: AssignmentDetailsDto;
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

  get isInterpreterLoaded() {
    return this.pythonService.isLoaded$();
  }

  get suggestions() {
    return this.assignment.suggestions;
  }

  executeSolution() {
    this.pythonService.executeCode(this.solution).subscribe(value => this.output = value);
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
