using MediatR;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin.Requests;

public record GetAssignmentStatusQuery(int UserId, int ExerciseId)
    : IRequest<GetAssignmentStatusResponse>;