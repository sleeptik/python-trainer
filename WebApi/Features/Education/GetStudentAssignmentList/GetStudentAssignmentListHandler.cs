using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.Education.GetStudentAssignmentList;

public class GetStudentAssignmentListHandler(ApplicationDbContext context)
    : IRequestHandler<GetStudentAssignmentListRequest, IList<Assignment>>
{
    public async Task<IList<Assignment>> Handle(GetStudentAssignmentListRequest request, CancellationToken cancellationToken)
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