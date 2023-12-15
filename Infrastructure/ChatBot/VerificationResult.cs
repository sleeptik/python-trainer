namespace Infrastructure.ChatBot;

public record VerificationResult(bool Valid, IList<string>? Errors, IList<string>? Suggestions);