import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-trainer-menu',
  templateUrl: './trainer-menu.component.html',
  styleUrl: './trainer-menu.component.css'
})
export class TrainerMenuComponent {
  @Output() readonly execute = new EventEmitter<void>();
  @Output() readonly clear = new EventEmitter<void>();
  @Output() readonly verify = new EventEmitter<void>();
}
