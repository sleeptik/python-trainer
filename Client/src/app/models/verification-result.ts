export interface VerificationResult {
  isCorrect: boolean;
  mistakes: {mistake : string, fixSuggestion : string | undefined}[];
}
