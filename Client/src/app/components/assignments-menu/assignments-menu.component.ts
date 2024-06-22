import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Subject} from "../../models/subject";
import {Assignment} from "../../models/assignment";
import {FormBuilder, FormControl} from "@angular/forms";

@Component({
  selector: 'app-assignments-menu',
  templateUrl: './assignments-menu.component.html'
})
export class AssignmentsMenuComponent implements OnInit {
  @Input({required: true}) subjects!: Subject[];
  @Input({required: true}) assignments!: Assignment[];

  @Output() randomClicked = new EventEmitter<unknown>();
  @Output() selectedClicked = new EventEmitter<Subject>();

  selectForm: FormControl<Subject | null>;

  constructor(private readonly formBuilder: FormBuilder) {
    this.selectForm = this.formBuilder.control(null);
  }

  ngOnInit(): void {
    setTimeout(() => this.selectForm = this.formBuilder.control(this.subjects[0]));
  }

  //Получения количества всех назначенных заданий
  get total(): number {
    return this.assignments.length;
  }

  //Получение количества решенных назначенныз заданий
  get totalFinished(): number {
    return this.assignments.filter(value => value.assignmentStatus.name!=="New").length

    // return this.assignments.reduce(
    //   (previousValue, currentValue) => previousValue + (currentValue.finishedAt ? 1 : 0), 0
    // );
  }

  //Получение количества правильно решенных назначенных заданий
  get totalSuccessfullyCompleted(): number {
    return this.assignments.filter(value => value.assignmentStatus.name==="Verified").length


    // return this.assignments.reduce(
    //   (previousValue, currentValue) => previousValue + (currentValue.isPassed === true ? 1 : 0), 0
    // );
  }
}
