import {CodeMistake} from "./code-mistake";

export interface VerificationResult {
  isCorrect: boolean;
  mistakes: CodeMistake[];
}
