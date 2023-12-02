using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.EducationAdmin.Notifications;
using WebApi.Features.EducationAdmin.Services;

namespace WebApi.Features.EducationAdmin.Handlers;

public class UpdateRankHandler(ApplicationDbContext context, RankService rankService)
    : INotificationHandler<AssignmentVerifiedNotification>
{
    public async Task Handle(AssignmentVerifiedNotification notification, CancellationToken cancellationToken)
    {
        var student = (await context.Students.FindAsync(notification.UserId))!;

        var change = 1.0f;
        var coefficient = 1.0f;

        coefficient *= await GetCurrentResultCoefficient(notification, cancellationToken);
        coefficient *= await GetPastResultsCoefficient(notification, cancellationToken);
        if (coefficient > 0) coefficient *= await GetStudentHighScoreCoefficient(notification, cancellationToken);

        change *= coefficient;
        student.Score += change;

        student.CurrentRankId = await rankService.GetUpdatedRankId(
            student.CurrentRankId, student.Score, cancellationToken
        );

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<float> GetCurrentResultCoefficient(AssignmentVerifiedNotification notification,
        CancellationToken cancellationToken)
    {
        var assignment = await context.Assignments.AsNoTracking()
            .FirstAsync(assignment =>
                    assignment.StudentId == notification.UserId
                    && assignment.ExerciseId == notification.ExerciseId,
                cancellationToken
            );

        // IsPassed should be already set at this moment
        return assignment.IsPassed!.Value ? 1.0f : 0.9f;
    }

    private async Task<float> GetPastResultsCoefficient(AssignmentVerifiedNotification notification,
        CancellationToken cancellationToken)
    {
        var coefficient = await context.Assignments.AsNoTracking()
            .Where(assignment => assignment.StudentId == notification.UserId)
            .OrderByDescending(assignment => assignment.FinishedAt) // should order by FinishedAt or AssignedAt???
            .SkipLast(1)
            .TakeLast(4)
            .Select(assignment => assignment.IsPassed)
            .Where(b => b.HasValue)
            .Cast<bool>()
            .Select(b => b ? .025f : -0.025f)
            .SumAsync(cancellationToken);

        return Math.Clamp(coefficient, 0.9f, 1.1f);
    }

    private async Task<float> GetStudentHighScoreCoefficient(AssignmentVerifiedNotification notification,
        CancellationToken cancellationToken)
    {
        var score = (await context.Students.FindAsync(
                new object[] { notification.UserId }, cancellationToken: cancellationToken)
            )!.Score;

        // TODO replace with lowest rank lower bound and highest rank upper bound
        var minScore = rankService.LowestLowerBound;
        var maxScore = rankService.HighestUpperBound;

        var coefficient = 1 - 0.2f * ((Math.Clamp(score, minScore, maxScore) - minScore) / (maxScore - minScore));

        return coefficient;
    }
}