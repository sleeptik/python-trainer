using Trainer.Verification.ChatBot;
using Trainer.Verification.InputData;
using Trainer.Verification.Python;

namespace Trainer.Verification;

public class VerificationService(
    SourceCodeCompilingService compilingService,
    AiVerificationService aiVerificationService
)
{
    public async void VerifyAsync(
        VerificationInstructionsSet instructionsSet, CancellationToken cancellationToken = default
    )
    {
        var compilationResult = await compilingService.VerifyAsync(instructionsSet.Solution, cancellationToken);

        if (!compilationResult.CanCompile)
            return; // TODO придумать что тут можно сделать

        var verificationResult = await aiVerificationService.VerifyAsync(instructionsSet, cancellationToken);

        // TODO Додумать что вернуть
    }
}