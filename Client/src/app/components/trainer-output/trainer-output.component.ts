import {Component, ElementRef, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ITheme, Terminal} from '@xterm/xterm';

const theme: ITheme = {
  background: "#ffffff",
  black: "#000000",
};

@Component({
  selector: 'app-trainer-output',
  templateUrl: './trainer-output.component.html'
})
export class TrainerOutputComponent implements OnInit, OnDestroy {
  @ViewChild("terminal", {static: true}) readonly terminalElementRef!: ElementRef;
  readonly terminal = new Terminal({disableStdin: true, rows: 8, theme: theme});

  @Input() set content(data: string) {
    this.terminal.clear();
    this.terminal.input(data, false);
  }

  ngOnInit(): void {
    this.terminal.open(this.terminalElementRef.nativeElement);
  }

  ngOnDestroy(): void {
    this.terminal.dispose();
  }
}
