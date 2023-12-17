import {Component, Input} from '@angular/core';
import {Exercise} from "../../models/exercise";
import {Assignment} from "../../models/assignment";

@Component({
  selector: 'app-assignment-details',
  templateUrl: './assignment-details.component.html'
})
export class AssignmentDetailsComponent {
  @Input({required: true}) assignment!: Assignment;
}
