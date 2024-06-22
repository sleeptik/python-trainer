using Trainer.Verification.ChatBot;
using Trainer.Verification.ChatBot.ResultModels;
using Trainer.Verification.InputData;
using Trainer.Verification.Python;

namespace Trainer.Verification;

public class VerificationService(
    SourceCodeCompilingService compilingService,
    AiVerificationService aiVerificationService
)
{
    //Сервис для верификации решений, сначала проверятеся возможность скомпилировать код, если успешно -> отправляем нейросети на проверку
    public async Task<VerificationResult> VerifyAsync(
        VerificationInstructionsSet instructionsSet, CancellationToken cancellationToken = default
    )
    {
        var compilationResult = await compilingService.CompileAsync(instructionsSet.Solution, cancellationToken);

        if (!compilationResult.CanCompile)
            return new VerificationResult(
                compilationResult.CanCompile,
                new List<CodeMistake> { new(compilationResult.CompilationError!, null) }
            );

        var verificationResult = await aiVerificationService.VerifyAsync(instructionsSet, cancellationToken);
        return verificationResult;
    }
}