using Trainer.Database;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification;
using Trainer.Verification.InputData;
using Trainer.WebApi.Features.Education.SetAssignmentSolution;

namespace Trainer.WebApi;

public class FooClass(TrainerContext context, VerificationService verifyingService)
{
    public async Task<Solution> Helper(SetAssignmentSolutionRequest request)
    {
        var assignment = await context.Assignments.FindAsync(request.StudentId, request.ExerciseId);

        if (assignment is null) throw new InvalidOperationException("Assignment not found");

        var solution = Solution.Create(request.Solution);
        assignment.AddSolution(solution);
        await context.SaveChangesAsync();
        var customInstructions = context.Prompts
            .Where(prompt => solution.Assignment.Exercise.Subjects.Any(subject => subject.Id == prompt.SubjectId))
            .Select(prompt => prompt.Content)
            .ToList();
        try
        {
            var result = await verifyingService.VerifyAsync(
                new VerificationInstructionsSet(solution.Assignment.Exercise.Details, solution.Code,
                    customInstructions));
            if (result.IsCorrect)
            {
                var review = new ValidatedReview();
                solution.SetReview(review);
            }
            else
            {
                var review = FaultyReview.Create(
                    result.Mistakes.Select(mistake =>
                        Suggestion.Create(mistake.Mistake, mistake.FixSuggestion))
                        .ToList());

                solution.SetReview(review);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        // assignment.SetResult(result.Valid);
        // await context.SaveChangesAsync(cancellationToken);

        return solution;
    }
}