using Infrastructure;
using MediatR;
using WebApi.Features.EducationAdmin.Requests;

namespace WebApi.Features.EducationAdmin.Handlers;

public class GetExerciseHistoryStatusHandler(ApplicationDbContext context)
    : IRequestHandler<GetExerciseHistoryStatus, Response.GetExerciseHistoryStatus>
{
    public async Task<Response.GetExerciseHistoryStatus> Handle(GetExerciseHistoryStatus request,
        CancellationToken cancellationToken)
    {
        var storedHistory = await context.Assignments.FindAsync(request.UserId, request.ExerciseId);
        return new Response.GetExerciseHistoryStatus(
            storedHistory is not null,
            storedHistory?.IsPassed ?? false
        );
    }
}