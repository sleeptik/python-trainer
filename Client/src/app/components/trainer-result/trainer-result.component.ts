import {Component, Input} from '@angular/core';
import {Suggestion} from "../../models/suggestion";

@Component({
  selector: 'app-trainer-result',
  templateUrl: './trainer-result.component.html'
})
export class TrainerResultComponent {
  @Input() result!: Suggestion[] | null;
}
