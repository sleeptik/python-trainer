using Trainer.Database.Entities.Assignments;
using Trainer.Verification.ChatBot.ResultModels;

namespace Trainer.WebApi.Services;

public static class ReviewFactory
{
    public static Review Create(VerificationResult result)
    {
        if (result.IsCorrect)
            return new ValidatedReview();

        var suggestions = result.Mistakes
            .Select(mistake => Suggestion.Create(mistake.Mistake, mistake.FixSuggestion))
            .ToList();

        return FaultyReview.Create(suggestions);
    }
}