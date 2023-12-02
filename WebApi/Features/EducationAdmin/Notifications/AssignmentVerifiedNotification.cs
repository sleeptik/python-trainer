using MediatR;

namespace WebApi.Features.EducationAdmin.Notifications;

public record AssignmentVerifiedNotification(int UserId, int ExerciseId) : INotification;