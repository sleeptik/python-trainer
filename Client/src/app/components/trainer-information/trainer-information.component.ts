import {Component, Input} from '@angular/core';
import {Exercise} from "../../models/exercise";

@Component({
  selector: 'app-trainer-information',
  templateUrl: './trainer-information.component.html'
})
export class TrainerInformationComponent {
  @Input({required: true}) exercise!: Exercise;
}
