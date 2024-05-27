using Microsoft.EntityFrameworkCore;
using Trainer.Database;
using Trainer.WebApi.Services;

namespace Trainer.WebApi.Controllers.Education.SetAssignmentSolution;

public class UpdateRankHandler(TrainerContext context, RankService rankService)
{
    public async Task Handle(AssignmentSolutionVerifiedNotification notification, CancellationToken cancellationToken)
    {
        var student = (await context.Students.FindAsync(notification.StudentId))!;

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

    private async Task<float> GetCurrentResultCoefficient(AssignmentSolutionVerifiedNotification notification,
        CancellationToken cancellationToken)
    {
        var assignment = await context.Assignments.AsNoTracking()
            .FirstAsync(assignment =>
                    assignment.StudentId == notification.StudentId
                    && assignment.ExerciseId == notification.ExerciseId,
                cancellationToken
            );

        // IsPassed should be already set at this moment
        return assignment.Solutions.First().Review!.IsCorrect ? 1.0f : -0.9f;
    }

    private async Task<float> GetPastResultsCoefficient(AssignmentSolutionVerifiedNotification notification,
        CancellationToken cancellationToken)
    {
        var change = await   context.Solutions.AsNoTracking()
            .Where(solution => solution.Assignment.StudentId == notification.StudentId)
            .Where(solution => solution.Review != null)
            .OrderBy(solution => solution.SubmittedAt)
            .GroupBy(solution => solution.AssignmentId)
            .Select(grouping => grouping.First())
            .Skip(1)
            .Take(4)
            .Select(solution => solution.Review)
            .Select(review => review.IsCorrect)
            .Cast<bool>()
            .Select(b => b ? 0.025f : -0.025f)
            .SumAsync(cancellationToken);

        return Math.Clamp(1f + change, 0.9f, 1.1f);
    }

    private async Task<float> GetStudentHighScoreCoefficient(AssignmentSolutionVerifiedNotification notification,
        CancellationToken cancellationToken)
    {
        var score = (await context.Students.FindAsync(
                new object[] { notification.StudentId }, cancellationToken)
            )!.Score;

        var minScore = rankService.LowestLowerBound;
        var maxScore = rankService.HighestUpperBound;

        var coefficient = 1 - 0.2f * ((Math.Clamp(score, minScore, maxScore) - minScore) / (maxScore - minScore));

        return coefficient;
    }
}