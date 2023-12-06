using MediatR;

namespace WebApi.Features.EducationAdmin.Notifications;

public record AssignmentVerifiedNotification(int StudentId, int ExerciseId) : INotification;