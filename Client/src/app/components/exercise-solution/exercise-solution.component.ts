import {Component, ElementRef, EventEmitter, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';

import * as AceEditor from "ace-builds";

import {Mode as Python} from "ace-builds/src-noconflict/mode-python";
import * as Theme from "ace-builds/src-noconflict/theme-dracula";
import "ace-builds/src-noconflict/ext-inline_autocomplete";

@Component({
  selector: 'app-exercise-solution',
  templateUrl: './exercise-solution.component.html'
})
export class ExerciseSolutionComponent implements OnInit, OnDestroy {
  @Output() readonly solve = new EventEmitter<string>();

  @ViewChild('editor', {static: true}) editorElement!: ElementRef<HTMLElement>;
  editor!: AceEditor.Ace.Editor;

  solved() {
    this.solve.emit(this.editor.getValue());
  }

  ngOnInit(): void {
    this.editor = AceEditor.edit(this.editorElement.nativeElement);

    this.editor.setFontSize(14);
    this.editor.setTheme(Theme);
    this.editor.getSession().setMode(new Python());

    this.editor.setOptions({
      enableBasicAutocompletion: true,
    });
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }
}
