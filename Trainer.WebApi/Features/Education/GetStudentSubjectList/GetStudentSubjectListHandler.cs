using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Trainer.WebApi.Features.Education.GetStudentSubjectList;

public class GetStudentSubjectListHandler(ApplicationDbContext context)
    : IRequestHandler<GetStudentSubjectListRequest, IList<Subject>>
{
    public async Task<IList<Subject>> Handle(GetStudentSubjectListRequest request, CancellationToken cancellationToken)
    {
        return await context.Students.AsNoTracking()
            .Include(student => student.SubjectsToStudy)
            .SelectMany(student => student.SubjectsToStudy)
            .ToListAsync(cancellationToken);
    }
}