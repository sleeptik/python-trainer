using Infrastructure;
using MediatR;
using WebApi.Features.Education.Requests;

namespace WebApi.Features.Education.Handlers;

public class FinishExerciseHandler(ApplicationDbContext context) : IRequestHandler<FinishExerciseCommand>
{
    public async Task Handle(FinishExerciseCommand request, CancellationToken cancellationToken)
    {
        var assignment = (await context.Assignments
            .FindAsync(new object[] { request.UserId, request.ExerciseId }, cancellationToken))!;

        assignment.Finish();

        await context.SaveChangesAsync(cancellationToken);
    }
}