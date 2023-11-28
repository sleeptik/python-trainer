using MediatR;

namespace WebApi.Features.EducationAdmin.Notifications;

public record ExerciseHistoryCreatedNotification(int UserId, bool IsPassed) : INotification;