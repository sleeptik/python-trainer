using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Quartz;
using Trainer.Database;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification;
using Trainer.Verification.InputData;

namespace Trainer.WebApi.Jobs;

public sealed class VerifySolutionsJob(
    TrainerContext trainerContext,
    VerificationService verificationService
) : IJob
{
    private const int VerifiedAtOnceCount = 10;
    private const int ResiliencyRetryAttempts = 3;

    private static readonly TimeSpan ResilienceDelay = TimeSpan.FromSeconds(15);

    public async Task Execute(IJobExecutionContext context)
    {
        var resiliencePipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                MaxRetryAttempts = ResiliencyRetryAttempts,
                Delay = ResilienceDelay
            })
            .Build();

        await resiliencePipeline.ExecuteAsync(VerifySolutionAsync);
    }

    private async ValueTask VerifySolutionAsync(CancellationToken cancellationToken)
    {
        var unverifiedSolutions = await trainerContext.Solutions
            .Include(solution => solution.Assignment)
            .ThenInclude(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .Where(solution => solution.Review == null)
            .Take(VerifiedAtOnceCount)
            .OrderBy(solution => solution.SubmittedAt)
            .ToListAsync(cancellationToken);

        var prompts = await trainerContext.Prompts.AsNoTracking()
            .ToListAsync(cancellationToken);

        var processableTasks = unverifiedSolutions
            .Select(async solution =>
            {
                var customInstructions = prompts
                    .Where(prompt => solution.Assignment.Exercise.Subjects
                        .Any(exerciseSubject => exerciseSubject.Id == prompt.SubjectId)
                    )
                    .Select(prompt => prompt.Content)
                    .ToList();

                var instructionsSet = new VerificationInstructionsSet(
                    solution.Assignment.Exercise.Details, solution.Code, customInstructions
                );

                var result = await verificationService.VerifyAsync(instructionsSet, cancellationToken);

                Review review = result.IsCorrect ? new ValidatedReview() : new FaultyReview();

                solution.SetReview(review);
            })
            .ToList();

        try
        {
            await Task.WhenAll(processableTasks);
        }
        finally
        {
            await trainerContext.SaveChangesAsync(cancellationToken);
        }
    }
}