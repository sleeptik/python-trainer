using MediatR;

namespace WebApi.Features.EducationAdmin.Requests;

public record SetHistoryStatusCommand(int UserId, int ExerciseId, bool Status) : IRequest;