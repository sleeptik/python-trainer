import {Injectable} from '@angular/core';
import {loadPyodide, PyodideInterface} from "pyodide";

@Injectable({
  providedIn: 'root'
})
export class PyodideService {
  private pyodide: PyodideInterface | undefined;
  private stdout: Array<string> = [];

  private async initialize() {
    const indexUrl = "https://cdn.jsdelivr.net/pyodide/v0.23.4/full/";
    this.pyodide = await loadPyodide({indexURL: indexUrl});
    this.pyodide.setStdout({batched: msg => this.stdout.push(msg)});
    this.pyodide.setStderr({batched: msg => console.error(msg)});
  }

  async runPythonAsync(code: string) {
    if (!this.pyodide)
      await this.initialize();

    this.stdout = [];

    let expression: string | undefined = undefined;

    try {
      expression = await this.pyodide!.runPythonAsync(code);
      if (expression) this.stdout.push(expression);
    } catch (e: unknown) {
      if (e instanceof this.pyodide!.ffi.PythonError)
        this.stdout = [e.message];
    }

    return this.stdout;
  }
}
