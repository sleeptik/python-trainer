using Trainer.Database;
using Trainer.Verification.ChatBot;
using Trainer.Verification.ChatBot.ResultModels;

namespace Trainer.WebApi.Features.Education.SetAssignmentSolution;

public class SetAssignmentSolutionHelper(TrainerContext context, AiVerificationService verifyingService)
{
    public async Task<VerificationResult> Handle(SetAssignmentSolutionRequest request,
        CancellationToken cancellationToken)
    {
        var assignment = await context.Assignments.FindAsync(
            new object[] { request.StudentId, request.ExerciseId },
            cancellationToken
        );

        if (assignment is null) throw new InvalidOperationException("Assignment not found");

        assignment.Finish(request.Solution);
        await context.SaveChangesAsync(cancellationToken);

        var result = await verifyingService.VerifyAsync(request.StudentId, request.ExerciseId, cancellationToken);

        assignment.SetResult(result.Valid);
        await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}