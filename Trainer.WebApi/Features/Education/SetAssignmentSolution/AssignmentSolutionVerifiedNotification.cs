using MediatR;

namespace Trainer.WebApi.Features.Education.SetAssignmentSolution;

public record AssignmentSolutionVerifiedNotification(int StudentId, int ExerciseId) : INotification;