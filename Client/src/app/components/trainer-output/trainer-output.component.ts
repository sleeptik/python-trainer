import {Component, ElementRef, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ITheme, Terminal} from '@xterm/xterm';

const theme: ITheme = {
  background: "#f2f2f2",
  foreground: "#000000"
};

@Component({
  selector: 'app-trainer-output',
  templateUrl: './trainer-output.component.html',
  styleUrl: './trainer-output.component.css'
})
export class TrainerOutputComponent implements OnInit, OnDestroy {
  @ViewChild("terminal", {static: true}) readonly terminalElementRef!: ElementRef;
  readonly terminal = new Terminal({disableStdin: true, rows: 12, theme: theme});

  @Input({required: true}) set content(data: string[]) {
    this.terminal.clear();

    if (data && data.length)
      data.forEach(value => this.terminal.writeln(value));
  }

  ngOnInit(): void {
    this.terminal.open(this.terminalElementRef.nativeElement);
  }

  ngOnDestroy(): void {
    this.terminal.dispose();
  }
}
