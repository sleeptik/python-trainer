using MediatR;

namespace WebApi.Features.Education.SetAssignmentSolution;

public record AssignmentSolutionVerifiedNotification(int StudentId, int ExerciseId) : INotification;