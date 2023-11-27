using MediatR;

namespace WebApi.Features.EducationAdmin.Requests;

public record GetExerciseHistoryStatus(int UserId, int ExerciseId)
    : IRequest<Response.GetExerciseHistoryStatus>;