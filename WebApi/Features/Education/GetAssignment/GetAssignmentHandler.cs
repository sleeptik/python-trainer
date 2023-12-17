using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.Education.GetAssignment;

public class GetAssignmentHandler(ApplicationDbContext context) : IRequestHandler<GetAssignmentRequest, Assignment>
{
    public async Task<Assignment> Handle(GetAssignmentRequest request, CancellationToken cancellationToken)
    {
        return await context.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Rank)
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .FirstAsync(
                assignment => assignment.StudentId == request.StudentId && assignment.ExerciseId == request.ExerciseId,
                cancellationToken
            );
    }
}