using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.Education.GetSubjects;

public class GetSubjectsHandler(ApplicationDbContext context)
    : IRequestHandler<GetSubjectsRequest, IList<Subject>>
{
    public async Task<IList<Subject>> Handle(GetSubjectsRequest request, CancellationToken cancellationToken)
    {
        return await context.Students.AsNoTracking()
            .Include(student => student.SubjectsToStudy)
            .SelectMany(student => student.SubjectsToStudy)
            .ToListAsync(cancellationToken);
    }
}