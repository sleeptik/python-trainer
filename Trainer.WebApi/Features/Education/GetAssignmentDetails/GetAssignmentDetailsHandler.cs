using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Trainer.WebApi.Features.Education.GetAssignmentDetails;

public class GetAssignmentDetailsHandler(ApplicationDbContext context) : IRequestHandler<GetAssignmentDetailsRequest, Assignment>
{
    public async Task<Assignment> Handle(GetAssignmentDetailsRequest detailsRequest, CancellationToken cancellationToken)
    {
        return await context.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Rank)
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .FirstAsync(
                assignment => assignment.StudentId == detailsRequest.StudentId && assignment.ExerciseId == detailsRequest.ExerciseId,
                cancellationToken
            );
    }
}