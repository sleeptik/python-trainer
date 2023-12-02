using Infrastructure;
using MediatR;
using WebApi.Features.EducationAdmin.Requests;

namespace WebApi.Features.EducationAdmin.Handlers;

public class SetAssignmentResultHandler(ApplicationDbContext context) : IRequestHandler<SetHistoryStatusCommand>
{
    public async Task Handle(SetHistoryStatusCommand request, CancellationToken cancellationToken)
    {
        var assignment = (await context.Assignments.FindAsync(request.StudentId, request.ExerciseId))!;

        assignment.SetResult(request.Status);

        context.Assignments.Update(assignment);
        await context.SaveChangesAsync(cancellationToken);
    }
}