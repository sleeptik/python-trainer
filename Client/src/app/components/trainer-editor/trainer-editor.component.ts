import {Component} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";

@Component({
  selector: 'app-trainer-editor',
  templateUrl: './trainer-editor.component.html',
  styleUrl: './trainer-editor.component.css'
})
export class TrainerEditorComponent {
  readonly codeControl: FormControl<string>;
  readonly options = {
    automaticLayout: true,
    language: 'python',
    lineNumbersMinChars: 2,
    minimap: {enabled: false},
    theme: 'vs'
  } as const;

  constructor(formBuilder: FormBuilder) {
    this.codeControl = formBuilder.control("", {nonNullable: true});
  }
}
