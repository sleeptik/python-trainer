/// <reference lib="webworker" />

import {loadPyodide, PyodideInterface} from "pyodide";

export class PythonService {
  private pyodide: PyodideInterface | null = null;
  private output: string[] = [];

  async executeCode(code: string): Promise<string[]> {
    this.output = [];

    const pyodide = await this.getPyodideInstanceAsync();
    const expressionValue: string = await pyodide.runPythonAsync(code);

    if (!expressionValue)
      return this.output;

    return [...this.output, expressionValue];
  }

  private async getPyodideInstanceAsync() {
    if (!this.pyodide) {
      this.pyodide = await loadPyodide();
      this.pyodide.setStdout({batched: msg => this.output.push(msg)});
      this.pyodide.setStderr({batched: msg => this.output.push(msg)});
    }

    return this.pyodide;
  }
}

const pythonService = new PythonService();

addEventListener('message', async ({data}) => {
  const response = await pythonService.executeCode(data);
  postMessage(response);
});
