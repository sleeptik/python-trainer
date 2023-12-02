using MediatR;

namespace WebApi.Features.EducationAdmin.Requests;

public record SetHistoryStatusCommand(int StudentId, int ExerciseId, bool Status) : IRequest;