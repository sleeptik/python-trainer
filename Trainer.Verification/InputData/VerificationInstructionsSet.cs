namespace Trainer.Verification.InputData;

public record VerificationInstructionsSet(string Task, string Solution, IList<string> CustomInstructions);