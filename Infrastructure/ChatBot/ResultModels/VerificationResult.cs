namespace Infrastructure.ChatBot.ResultModels;

public sealed record VerificationResult(bool IsCorrect, IList<CodeMistake> Mistakes);