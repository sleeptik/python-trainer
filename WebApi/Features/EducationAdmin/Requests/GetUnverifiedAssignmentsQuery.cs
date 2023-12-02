using MediatR;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin.Requests;

public record GetUnverifiedAssignmentsQuery(int UserId, int ExerciseId) : IRequest<IList<GetUnverifiedAssignmentResponse>>;