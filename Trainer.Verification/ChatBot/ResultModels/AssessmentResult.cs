namespace Trainer.Verification.ChatBot.ResultModels;

public sealed record AssessmentResult(
    int Readability,
    int Complexity,
    int Creativity,
    int Efficiency,
    int Structure,
    int Logic);