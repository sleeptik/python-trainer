import {Injectable} from '@angular/core';
import {from, map} from "rxjs";
import {loadPyodide, PyodideInterface} from "pyodide";

@Injectable({
  providedIn: 'root'
})
export class PythonService {
  private pyodide!: PyodideInterface;

  constructor() {
    from(loadPyodide({indexURL:"/assets/pyodide"})).subscribe(value => this.pyodide = value);
  }

  executeCode(code: string) {
    const output: string[] = [];
    this.pyodide.setStdout({batched: msg => output.push(msg)});
    this.pyodide.setStderr({batched: msg => output.push(msg)});

    return from(this.pyodide.runPythonAsync(code)).pipe(
      map((expressionValue: string | null) => {
        return expressionValue ? [...output, expressionValue] : output;
      })
    );
  }
}
