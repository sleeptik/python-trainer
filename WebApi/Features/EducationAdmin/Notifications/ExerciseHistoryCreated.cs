using MediatR;

namespace WebApi.Features.EducationAdmin.Notifications;

public record ExerciseHistoryCreated(int UserId, bool IsPassed) : INotification;