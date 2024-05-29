using Microsoft.EntityFrameworkCore;
using Trainer.Database;
using Trainer.WebApi.Services;

namespace Trainer.WebApi.Controllers.Education.SetAssignmentSolution;

public class UpdateRankHelper(TrainerContext context, RankService rankService)
{
    public async Task UpdateRank(int studentId,int exercieId, CancellationToken cancellationToken=default)
    {
        var student = (await context.Students.FindAsync(studentId));

        var change = 1.0f;
        var coefficient = 1.0f;

        coefficient *= await GetCurrentResultCoefficient(studentId,exercieId, cancellationToken);
        coefficient *= await GetPastResultsCoefficient(studentId, cancellationToken);
        if (coefficient > 0) coefficient *= await GetStudentHighScoreCoefficient(studentId, cancellationToken);

        change *= coefficient;
        student.Score += change;

        student.CurrentRankId = await rankService.GetUpdatedRankId(
            student.CurrentRankId, student.Score, cancellationToken
        );

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<float> GetCurrentResultCoefficient(int studentId,int exerciseId,
        CancellationToken cancellationToken)
    {
        var assignment = await context.Assignments.AsNoTracking()
            .FirstAsync(assignment =>
                    assignment.StudentId == studentId
                    && assignment.ExerciseId == exerciseId,
                cancellationToken
            );

        // IsPassed should be already set at this moment
        return assignment.Solutions.First().Review!.IsCorrect ? 1.0f : -0.9f;
    }

    private async Task<float> GetPastResultsCoefficient(int studentId,
        CancellationToken cancellationToken)
    {
        var change = await   context.Solutions.AsNoTracking()
            .Where(solution => solution.Assignment.StudentId == studentId)
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

    private async Task<float> GetStudentHighScoreCoefficient(int studentId,
        CancellationToken cancellationToken)
    {
        var score = (await context.Students.FindAsync(
                new object[] { studentId }, cancellationToken)
            )!.Score;

        var minScore = rankService.LowestLowerBound;
        var maxScore = rankService.HighestUpperBound;

        var coefficient = 1 - 0.2f * ((Math.Clamp(score, minScore, maxScore) - minScore) / (maxScore - minScore));

        return coefficient;
    }
}