using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.Education.GetAssignments;

public class GetAssignmentsHandler(ApplicationDbContext context)
    : IRequestHandler<GetAssignmentsRequest, IList<Assignment>>
{
    public async Task<IList<Assignment>> Handle(GetAssignmentsRequest request, CancellationToken cancellationToken)
    {
        return await context.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Rank)
            .Where(assignment => assignment.StudentId == request.StudentId)
            .ToListAsync(cancellationToken);
    }
}