import {Component, ElementRef, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {Terminal} from '@xterm/xterm';

@Component({
  selector: 'app-trainer-output',
  templateUrl: './trainer-output.component.html'
})
export class TrainerOutputComponent implements OnInit, OnDestroy {
  @ViewChild("terminal", {static: true}) readonly terminalElementRef!: ElementRef;
  readonly terminal = new Terminal({disableStdin: true});

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
