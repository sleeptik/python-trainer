using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.EducationAdmin.Requests;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin.Handlers;

public class GetUnverifiedAssignmentsHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetUnverifiedAssignmentsQuery, IList<GetUnverifiedAssignmentResponse>>
{
    public async Task<IList<GetUnverifiedAssignmentResponse>> Handle(GetUnverifiedAssignmentsQuery query,
        CancellationToken cancellationToken)
    {
        var unverified = await context.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .Where(assignment => assignment.IsPassed == null)
            .ToListAsync(cancellationToken);

        var response = mapper.Map<IList<GetUnverifiedAssignmentResponse>>(unverified);

        return response;
    }
}