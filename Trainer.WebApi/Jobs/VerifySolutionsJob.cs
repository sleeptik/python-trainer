using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Quartz;
using Trainer.Database;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification;
using Trainer.Verification.InputData;
using Trainer.WebApi.Services;

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

        var state = new State(trainerContext, verificationService);

        await resiliencePipeline.ExecuteAsync(
            async (ctx, token) => await VerifySolutionAsync(ctx, token), state
        );
    }

    private static async ValueTask VerifySolutionAsync(State ctx, CancellationToken cancellationToken)
    {
        if (!ctx.Context.Solutions.Any(solution => solution.Review == null))
            return;

        var unverifiedSolutions = await ctx.Context.Solutions
            .Include(solution => solution.Assignment)
            .ThenInclude(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .Where(solution => solution.Review == null)
            .Take(VerifiedAtOnceCount)
            .OrderBy(solution => solution.SubmittedAt)
            .ToListAsync(cancellationToken);

        var prompts = await ctx.Context.Prompts.AsNoTracking()
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

                var result = await ctx.Service.VerifyAsync(instructionsSet, cancellationToken);

                var review = ReviewFactory.Create(result);

                AssignmentStatus assignmentStatus;
                if (review is FaultyReview faultyReview)
                {
                    ctx.Context.Suggestions.AttachRange(faultyReview.Suggestions);
                    assignmentStatus = await ctx.Context.AssignmentStatuses
                        .Where(status => status.Name == AssignmentStatus.Failed).FirstAsync(cancellationToken);
                    solution.Assignment.SetStatus(assignmentStatus.Id);
                }
                else
                {
                    assignmentStatus = await ctx.Context.AssignmentStatuses
                        .Where(status => status.Name == AssignmentStatus.Verified).FirstAsync(cancellationToken);
                    solution.Assignment.SetStatus(assignmentStatus.Id);
                }

                    ctx.Context.Reviews.Attach(review);

                solution.SetReview(review);
            })
            .ToList();

        try
        {
            await Task.WhenAll(processableTasks);
        }
        finally
        {
            await ctx.Context.SaveChangesAsync(cancellationToken);
            ctx.Context.ChangeTracker.Clear();
        }
    }

    private class State(TrainerContext context, VerificationService service)
    {
        public TrainerContext Context { get; } = context;
        public VerificationService Service { get; } = service;
    }
}