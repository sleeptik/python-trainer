using Infrastructure;
using Infrastructure.ChatBot;
using MediatR;

namespace Trainer.WebApi.Features.Education.SetAssignmentSolution;

public class SetAssignmentSolutionHandler(ApplicationDbContext context, SolutionVerifyingService verifyingService)
    : IRequestHandler<SetAssignmentSolutionRequest, VerificationResult>
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