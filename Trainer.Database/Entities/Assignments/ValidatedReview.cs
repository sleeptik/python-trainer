// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Assignments;

public sealed class ValidatedReview : Review
{
    public override bool IsCorrect => true;

    public Assessment? Assessment { get; private set; }
}