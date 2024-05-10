// ReSharper disable ClassNeverInstantiated.Global

namespace Infrastructure.ChatBot.ResultModels;

public sealed record CodeMistake(string Mistake, string? FixSuggestion);