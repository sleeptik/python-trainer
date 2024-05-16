// ReSharper disable ClassNeverInstantiated.Global

namespace Trainer.Verification.ChatBot.ResultModels;

public sealed record CodeMistake(string Mistake, string? FixSuggestion);