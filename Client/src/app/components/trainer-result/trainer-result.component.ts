import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-trainer-result',
  templateUrl: './trainer-result.component.html'
})
export class TrainerResultComponent {
  @Input() result!: string | null;
}
