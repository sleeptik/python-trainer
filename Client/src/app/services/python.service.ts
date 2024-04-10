import {Injectable} from '@angular/core';
import {Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PythonService {
  worker: Worker;
  response: Subject<string[]> = new Subject<string[]>();

  constructor() {
    this.worker = new Worker(new URL("../web-workers/python.worker", import.meta.url));
    this.worker.onmessage = ({data}) => this.response.next(data);
  }

  executeCode(code: string) {
    this.worker.postMessage(code);
  }

  getObservable() {
    return this.response.asObservable();
  }
}
