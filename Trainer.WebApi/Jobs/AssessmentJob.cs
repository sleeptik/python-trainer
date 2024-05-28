using Microsoft.EntityFrameworkCore;
using Microsoft.Scripting.Utils;
using Quartz;
using Trainer.Database;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification.ChatBot;
using Trainer.Verification.InputData;

namespace Trainer.WebApi.Jobs;

public class AssessmentJob(
    TrainerContext trainerContext,
    AssessmentService assessmentService
) : IJob
{
    private const int AssessedAtOnceCount = 3;

    private List<ValidatedReview> _unassessmentReview =  [];
    private Dictionary<ValidatedReview, Func<Task>> _processableAssess =  [];

    public async Task Execute(IJobExecutionContext context)
    {
        var assessments = await trainerContext.Assessments
            .Select(assessment => assessment.ReviewId)
            .ToListAsync();
        _unassessmentReview = await trainerContext.Reviews
            .Include(review => review.Solution)
            .ThenInclude(solution => solution.Assignment)
            .ThenInclude(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .OfType<ValidatedReview>()
            .Where(review => !assessments.Contains(review.Id))
            .OrderBy(review => review.Solution.SubmittedAt)
            .ToListAsync();

        while (_unassessmentReview.Count > 0)
        {
            TakeNewSolutionsToAssess();

            var assessmentTasks = _processableAssess.Select(pair => pair.Value.Invoke()).ToList();

            try
            {
                await Task.WhenAll(assessmentTasks);
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

    private void TakeNewSolutionsToAssess()
    {
        var toAssess = _unassessmentReview.Take(AssessedAtOnceCount).ToList();
        _unassessmentReview = _unassessmentReview.Skip(AssessedAtOnceCount).ToList();

        _processableAssess = toAssess.ToDictionary(
            review => review,
            review =>
            {
                return new Func<Task>(AssessAsync);

                async Task AssessAsync()
                {
                    var result = await assessmentService.AssessAsync(
                        review.Solution.Assignment.Exercise.Details, review.Solution.Code
                    );


                    review.SetAssessment(Assessment.Create(result.Readability, result.Complexity, result.Creativity,
                        result.Efficiency, result.Structure, result.Logic));
                    await trainerContext.SaveChangesAsync();
                }
            }
        );
    }
}