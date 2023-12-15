export interface VerificationResult {
  valid: boolean;
  errors: Array<string> | undefined;
  suggestions: Array<string> | undefined;
}
