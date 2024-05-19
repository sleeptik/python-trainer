using Trainer.Database;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification.ChatBot;
using Trainer.Verification.ChatBot.ResultModels;

namespace Trainer.WebApi.Features.Education.SetAssignmentSolution;

public class SetAssignmentSolutionHelper(TrainerContext context/*, AiVerificationService verifyingService*/)
{
    public async Task<Solution> Helper(SetAssignmentSolutionRequest request)
    {
        var assignment = await context.Assignments.FindAsync(
            new object[] { request.StudentId, request.ExerciseId }
        );

        if (assignment is null) throw new InvalidOperationException("Assignment not found");

        var solution = Solution.Create(request.Solution);
        assignment.AddSolution(solution);
        await context.SaveChangesAsync();

        // var result = await verifyingService.VerifyAsync(request.StudentId, request.ExerciseId, cancellationToken);

        // assignment.SetResult(result.Valid);
        // await context.SaveChangesAsync(cancellationToken);

        return solution;
    }
}