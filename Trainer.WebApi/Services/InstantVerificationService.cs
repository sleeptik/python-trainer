using Trainer.Database.Entities.Assignments;
using Trainer.Verification;
using Trainer.Verification.InputData;

namespace Trainer.WebApi.Services;

public sealed class InstantVerificationService(VerificationService verificationService)
{
    //Сервис с методом вызывающим методы для проверки решения пользователя и получения Review
    public async Task<Review> VerifyOnceOrThrowAsync(VerificationInstructionsSet instructionsSet)
    {
        var result = await verificationService.VerifyAsync(instructionsSet);

        var review = ReviewFactory.Create(result);

        return review;
    }
}