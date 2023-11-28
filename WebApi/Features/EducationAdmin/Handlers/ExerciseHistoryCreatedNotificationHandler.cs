using Domain.Trainer;
using Infrastructure;
using MediatR;
using WebApi.Features.Education.Services;
using WebApi.Features.EducationAdmin.Notifications;
using WebApi.Features.EducationAdmin.Services;

namespace WebApi.Features.EducationAdmin.Handlers;

public class ExerciseHistoryCreatedNotificationHandler
    (ApplicationDbContext context, HistoryService historyService) : INotificationHandler<
        ExerciseHistoryCreatedNotification>
{
    public async Task Handle(ExerciseHistoryCreatedNotification notification, CancellationToken cancellationToken)
    {
        var rank = await context.UserRanks.FindAsync(
            new object[] { notification.UserId }, cancellationToken
        );

        if (rank is null)
            throw new NotImplementedException();

        var change = notification.IsPassed ? 1 : -1;
        rank.Metric += change;
        var statisticOfTwoPast = historyService.GetUserHistory(notification.UserId)
            .SkipLast(1)
            .TakeLast(2)
            .ToList();
        
        var koef = 0f;

        statisticOfTwoPast.Select(history => koef+=history.IsPassed?0.5f:0);
        if (notification.IsPassed)
        {
            rank.Metric += (2-koef);
            if (rank.Metric >= 11f) rank.AssignedDifficultyId = 3;
            else if (rank.Metric >= 8f) rank.AssignedDifficultyId = 2; 
        }
        else
        {
            rank.Metric -= (1+koef);
            if (rank.Metric < 7F) rank.AssignedDifficultyId = 1;
            else if (rank.Metric < 10f) rank.AssignedDifficultyId = 2;
        }


        await context.SaveChangesAsync(cancellationToken);
    }
}