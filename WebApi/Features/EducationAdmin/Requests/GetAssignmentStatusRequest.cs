using MediatR;
using WebApi.Features.EducationAdmin.Response;

namespace WebApi.Features.EducationAdmin.Requests;

public record GetAssignmentStatusRequest(int UserId, int ExerciseId)
    : IRequest<GetAssignmentStatusResponse>;