import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-education-list',
  templateUrl: './education-list.component.html'
})
export class EducationListComponent {
  numbers!: number[];

  @Input({required: true}) set amount(amount: number) {
    this.numbers = Array(amount).fill(amount).map((_, index) => index + 1);
  };

  @Output() selectIndex = new EventEmitter<number>();
}
