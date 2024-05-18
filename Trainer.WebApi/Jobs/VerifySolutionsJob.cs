using Microsoft.EntityFrameworkCore;
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
    private const int VerifiedAtOnceCount = 3;

    private List<Solution> _unverifiedSolutions = [];
    private Dictionary<Solution, Func<Task>> _processableSolutions = [];


    public async Task Execute(IJobExecutionContext context)
    {
        _unverifiedSolutions = await trainerContext.Solutions
            .Include(solution => solution.Assignment)
            .ThenInclude(assignment => assignment.Exercise)
            .Where(solution => solution.Review == null)
            .OrderBy(solution => solution.SubmittedAt)
            .ToListAsync();

        while (_unverifiedSolutions.Count > 0)
        {
            TakeNewSolutionsToVerification();

            var verificationTasks = _processableSolutions.Select(pair => pair.Value.Invoke()).ToList();

            try
            {
                await Task.WhenAll(verificationTasks);
            }
            catch
            {
                break;
            }
            finally
            {
                await trainerContext.SaveChangesAsync();
            }
        }
    }

    private void TakeNewSolutionsToVerification()
    {
        var toVerification = _unverifiedSolutions.Take(VerifiedAtOnceCount).ToList();
        _unverifiedSolutions = _unverifiedSolutions.Skip(VerifiedAtOnceCount).ToList();

        _processableSolutions = toVerification.ToDictionary(
            solution => solution,
            solution =>
            {
                return new Func<Task>(VerifyAsync);

                async Task VerifyAsync()
                {
                    var result = await verificationService.VerifyAsync(
                        new VerificationInstructionsSet(solution.Assignment.Exercise.Details, solution.Code)
                    );

                    Review review = result.IsCorrect ? new ValidatedReview() : new FaultyReview();

                    solution.SetReview(review);
                }
            }
        );
    }
}