import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html'
})
export class TrainerComponent {
  readonly assignment!: Assignment;
  outputContent: string = "";

  constructor(
    activatedRoute: ActivatedRoute,
  ) {
    this.assignment = activatedRoute.snapshot.data["assignment"];
  }

  async executeCode() {
  }

  verifySolution() {

  }

  clearOutput() {
    this.outputContent = "";
  }
}
