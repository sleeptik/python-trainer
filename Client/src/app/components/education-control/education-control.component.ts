import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl} from "@angular/forms";

interface Subject {
  id: number;
  name: string;
}

@Component({
  selector: 'app-education-control',
  templateUrl: './education-control.component.html'
})
export class EducationControlComponent implements OnInit {


  @Input({required: true}) subjects!: Array<Subject>;
  @Output() readonly request = new EventEmitter<number | null>();

  selected!: FormControl<number | null>;

  receiveSelected() {
    this.request.emit(this.selected.value);
  }

  ngOnInit(): void {
    this.selected = new FormControl<number>(this.subjects[0].id);
  }

  receiveRandom() {
    const random = this.subjects[Math.floor(Math.random() * this.subjects.length)];
    this.request.emit(random.id);
  }
}
