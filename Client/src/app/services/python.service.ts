import {Injectable} from '@angular/core';
import {BehaviorSubject, from, map, tap} from "rxjs";
import {loadPyodide, PyodideInterface} from "pyodide";

@Injectable({
  providedIn: 'root'
})
export class PythonService {
  private loading: BehaviorSubject<boolean> = new BehaviorSubject(false);
  private pyodide!: PyodideInterface;

  constructor() {
    setTimeout(() => {
      from(loadPyodide({indexURL: "/assets/pyodide"})).pipe(
        tap(value => this.pyodide = value),
        tap(_ => this.loading.next(true))
      ).subscribe();
    }, 0);
  }

  isLoaded$() {
    return this.loading.asObservable();
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
