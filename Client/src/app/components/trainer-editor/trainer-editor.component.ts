import {Component, Input} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-trainer-editor',
  templateUrl: './trainer-editor.component.html',
  styleUrl: './trainer-editor.component.css'
})
export class TrainerEditorComponent {
  @Input({required: true}) codeControl!: FormControl<string>;
  readonly options = {
    automaticLayout: true,
    language: 'python',
    lineNumbersMinChars: 2,
    minimap: {enabled: false},
    theme: 'vs'
  } as const;
}
