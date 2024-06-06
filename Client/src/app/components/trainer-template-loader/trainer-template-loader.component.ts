import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CodeTemplate} from "../../models/code-template";
import {StudentsService} from "../../services/students.service";
import {ExercisesService} from "../../services/exercises.service";
import {Student} from "../../models/student";

@Component({
  selector: 'app-trainer-template-loader',
  templateUrl: './trainer-template-loader.component.html'
})
export class TrainerTemplateLoaderComponent implements OnInit {
  @Input({required: true}) exerciseId!: number;
  @Output() templateLoaded: EventEmitter<string> = new EventEmitter();

  canUseTemplates: boolean = false;
  templates: CodeTemplate[] = [];
  student!: Student;

  constructor(
    private readonly studentsService: StudentsService,
    private readonly exercisesService: ExercisesService
  ) {
  }

  ngOnInit(): void {
    // Load short template info
    this.studentsService.getStudentMe().subscribe(value => {
      this.student = value;
      if (value.score<3){
        this.canUseTemplates = true
      }
    })
    if(!this.canUseTemplates){
      return
    }
    this.exercisesService.getCodeTemplates(this.exerciseId).subscribe(value => this.templates = value)
  }

  loadTemplate(template: CodeTemplate) {
    this.templateLoaded.emit(template.code);
  }
}
