using Infrastructure;
using MediatR;
using WebApi.Features.EducationAdmin.Notifications;

namespace WebApi.Features.EducationAdmin.Handlers;

public class ExerciseHistoryCreatedNotificationHandler
    (ApplicationDbContext context) : INotificationHandler<ExerciseHistoryCreated>
{
    public async Task Handle(ExerciseHistoryCreated notification, CancellationToken cancellationToken)
    {
        var rank = await context.UserRanks.FindAsync(
            new object[] { notification.UserId }, cancellationToken
        );

        if (rank is null)
            throw new NotImplementedException();

        var change = notification.IsPassed ? 1 : -1;
        rank.Metric += change;

        // TODO change user assigned difficulty so user can progress
        rank.AssignedDifficultyId++;
        rank.AssignedDifficultyId--;

        await context.SaveChangesAsync(cancellationToken);
    }
}