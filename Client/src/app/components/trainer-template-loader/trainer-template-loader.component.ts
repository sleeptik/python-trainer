import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CodeTemplate} from "../../models/code-template";

@Component({
  selector: 'app-trainer-template-loader',
  templateUrl: './trainer-template-loader.component.html'
})
export class TrainerTemplateLoaderComponent implements OnInit {
  @Input({required: true}) exerciseId!: number;
  @Output() templateLoaded: EventEmitter<string> = new EventEmitter();

  canUseTemplates: boolean = false;
  templates: CodeTemplate[] = [];

  ngOnInit(): void {
    // Load short template info
  }

  loadTemplate(templateId: number) {
    this.templateLoaded.emit(/*Value*/);
  }
}
