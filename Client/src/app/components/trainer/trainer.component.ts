import {Component} from '@angular/core';
import {Assignment} from "../../models/assignment";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html',
  styleUrl: './trainer.component.css'
})
export class TrainerComponent {
  readonly assignment!: Assignment;

  constructor(activatedRoute: ActivatedRoute) {
    this.assignment = activatedRoute.snapshot.data["assignment"];
  }
}
