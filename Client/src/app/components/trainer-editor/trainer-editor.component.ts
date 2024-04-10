import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-trainer-editor',
  templateUrl: './trainer-editor.component.html',
  styleUrl: './trainer-editor.component.css'
})
export class TrainerEditorComponent {
  @Output() readonly codeChange: EventEmitter<string> = new EventEmitter<string>();

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
    this.codeControl.valueChanges.pipe(takeUntilDestroyed()).subscribe(value => this.codeChange.emit(value));
  }
}
