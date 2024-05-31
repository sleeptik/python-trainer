import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {PythonService} from "../../services/python.service";
import {pipe, switchMap, tap} from "rxjs";
import {AssignmentsService} from "../../services/assignments.service";
import {AssignmentDetailsDto} from "../../models/assignment-details-dto";
import {FormBuilder, FormControl} from "@angular/forms";

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html'
})
export class TrainerComponent {
  assignment!: AssignmentDetailsDto;
  codeControl: FormControl<string>;
  output: string[] = [];


  constructor(
    activatedRoute: ActivatedRoute,
    formBuilder: FormBuilder,
    private readonly pythonService: PythonService,
    private readonly assignmentsService: AssignmentsService
  ) {
    this.assignment = activatedRoute.snapshot.data["assignment"];
    this.codeControl = formBuilder.control(this.assignment.solution?.code ?? "", {nonNullable: true});
  }

  get isInterpreterLoaded() {
    return this.pythonService.isLoaded$();
  }

  get suggestions() {
    return this.assignment.suggestions;
  }

  executeSolution() {
    this.pythonService.executeCode(this.codeControl.value).subscribe(value => this.output = value);
  }

  verifySolution() {
    this.assignmentsService.setAssignmentSolution(this.assignment.id, this.codeControl.value).pipe(this.refresh()).subscribe();
  }

  clearOutput() {
    this.output = [];
  }

  private refresh() {
    return pipe(
      switchMap(_ => this.assignmentsService.getAssignmentDetails(this.assignment.id)),
      tap(value => this.assignment = value),
      tap(_ => this.codeControl.setValue(this.assignment.solution?.code ?? ""))
    );
  }
}
