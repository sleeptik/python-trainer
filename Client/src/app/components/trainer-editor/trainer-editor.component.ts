import {Component} from '@angular/core';

@Component({
  selector: 'app-trainer-editor',
  templateUrl: './trainer-editor.component.html',
  styleUrl: './trainer-editor.component.css'
})
export class TrainerEditorComponent {
  readonly options = {
    automaticLayout: true,
    language: 'python',
    lineNumbersMinChars: 2,
    minimap: {enabled: false},
    theme: 'vs'
  };
}
