using MediatR;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin.Requests;

public record GetUnverifiedAssignmentsQuery : IRequest<IList<GetUnverifiedAssignmentResponse>>;