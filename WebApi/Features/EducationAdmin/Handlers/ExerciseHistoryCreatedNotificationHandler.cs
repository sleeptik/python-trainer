﻿using Infrastructure;
using MediatR;
using WebApi.Features.Education.Services;
using WebApi.Features.EducationAdmin.Notifications;

namespace WebApi.Features.EducationAdmin.Handlers;

public class ExerciseHistoryCreatedNotificationHandler
    (ApplicationDbContext context, AssignmentService assignmentService) : INotificationHandler<
        ExerciseHistoryCreatedNotification>
{
    public async Task Handle(ExerciseHistoryCreatedNotification notification, CancellationToken cancellationToken)
    {
        var rank = await context.Students.FindAsync(
            new object[] { notification.UserId }, cancellationToken
        );

        throw new NotImplementedException();

        await context.SaveChangesAsync(cancellationToken);
    }
}