using Infrastructure;
using MediatR;
using WebApi.Features.EducationAdmin.Requests;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin.Handlers;

public class GetAssignmentStatusHandler(ApplicationDbContext context)
    : IRequestHandler<GetAssignmentStatusQuery, GetAssignmentStatusResponse>
{
    public async Task<GetAssignmentStatusResponse> Handle(GetAssignmentStatusQuery query,
        CancellationToken cancellationToken)
    {
        var storedHistory = await context.Assignments.FindAsync(query.UserId, query.ExerciseId);
        return new GetAssignmentStatusResponse(
            storedHistory is not null,
            storedHistory?.IsPassed ?? false
        );
    }
}