using MediatR;

namespace WebApi.Features.Education.Requests;

public record FinishExerciseCommand(int UserId, int ExerciseId) : IRequest;