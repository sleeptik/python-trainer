using Infrastructure;
using MediatR;
using WebApi.Features.EducationAdmin.Requests;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin.Handlers;

public class GetAssignmentStatusHandler(ApplicationDbContext context)
    : IRequestHandler<GetAssignmentStatusRequest, GetAssignmentStatusResponse>
{
    public async Task<GetAssignmentStatusResponse> Handle(GetAssignmentStatusRequest request,
        CancellationToken cancellationToken)
    {
        var storedHistory = await context.Assignments.FindAsync(request.UserId, request.ExerciseId);
        return new GetAssignmentStatusResponse(
            storedHistory is not null,
            storedHistory?.IsPassed ?? false
        );
    }
}