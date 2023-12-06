using Infrastructure;
using Infrastructure.ChatBot;
using MediatR;
using WebApi.Features.Education.Requests;

namespace WebApi.Features.Education.Handlers;

public class FinishExerciseHandler(ApplicationDbContext context, SolutionVerifyingService verifyingService) : IRequestHandler<FinishExerciseCommand, VerificationResult>
{
    public async Task<VerificationResult> Handle(FinishExerciseCommand request, CancellationToken cancellationToken)
    {
        var assignment = (await context.Assignments
            .FindAsync(new object[] { request.StudentId, request.ExerciseId }, cancellationToken))!;

        assignment.Finish(request.Solution);

        var result = await verifyingService.VerifyAsync(request.StudentId, request.ExerciseId, cancellationToken);
        assignment.SetResult(result.Valid);

        await context.SaveChangesAsync(cancellationToken);
        return result;
    }
}