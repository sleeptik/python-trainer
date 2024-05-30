using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification.InputData;
using Trainer.WebApi.Common;
using Trainer.WebApi.Controllers.Education.DTO;
using Trainer.WebApi.Controllers.Education.SetAssignmentSolution;
using Trainer.WebApi.Controllers.Education.StudentSelfAssignment;
using Trainer.WebApi.Services;

namespace Trainer.WebApi.Controllers.Education;

[Route("api/education/assignments")]
public sealed class AssignmentsController(InstantVerificationService instantVerificationService, RankService rankService) : ApiController
{
    private int StudentId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1");

    [HttpGet("")]
    public async Task<IActionResult> GetStudentAssignments()
    {
        var assignments = await TrainerContext.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Rank)
            .Where(assignment => assignment.StudentId == StudentId)
            .ToListAsync();

        return Ok(assignments);
    }

    [HttpGet("{assignmentId:int}")]
    public async Task<IActionResult> GetAssignmentDetails(int assignmentId)
    {
        var assignment = await TrainerContext.Assignments.AsNoTracking()
                .Include(assignment => assignment.Exercise)
                .ThenInclude(exercise => exercise.Rank)
                .Include(assignment => assignment.Exercise)
                .ThenInclude(exercise => exercise.Subjects)
                .Include(
                    assignment => assignment.Solutions
                        .OrderByDescending(solution => solution.VerifiedAt)
                        .Take(1)
                )
                .ThenInclude(solution => solution.Review)
                .ThenInclude(review => ((FaultyReview)review!).Suggestions)
                .Select(assignment => new
                    {
                        assignment.Id,
                        assignment.Exercise,
                        Solution = assignment.Solutions.FirstOrDefault(),
                        Suggestions = assignment.Solutions
                            .SelectMany(solution => ((FaultyReview)solution.Review!).Suggestions)
                            .ToList(),
                    }
                )
                .SingleAsync(assignment => assignment.Id == assignmentId);

        return Ok(
            new AssignmentDetailsDto(assignment.Id, assignment.Exercise, assignment.Solution, assignment.Suggestions)
        );
    }

    [HttpPost("")]
    public async Task<IActionResult> AssignYourself(StudentSelfAssignmentDto dto)
    {
        var assignmentHelper = new StudentSelfAssignmentHelper(TrainerContext);

        await assignmentHelper.SelfAssignment(
            new StudentSelfAssignmentRequest(StudentId, dto.SubjectId)
        );

        return NoContent();
    }

    [HttpPost("{assignmentId:int}/solutions")]
    public async Task<IActionResult> SetAssignmentSolution(int assignmentId, SetAssignmentSolutionDto dto)
    {
        var assignment = await TrainerContext.Assignments
            .Include(assignment1 => assignment1.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .SingleAsync(assignment1 => assignment1.Id == assignmentId);

        var solution = Solution.Create(dto.Solution);
        assignment.AddSolution(solution);

        await TrainerContext.SaveChangesAsync();

        try
        {
            var subjectIds = solution.Assignment.Exercise.Subjects.Select(subject => subject.Id).ToList();

            var customInstructions = TrainerContext.Prompts
                .Where(prompt => subjectIds.Any(id => id == prompt.SubjectId))
                .Select(prompt => prompt.Content)
                .ToList();

            var instructions = new VerificationInstructionsSet(
                assignment.Exercise.Details, dto.Solution, customInstructions
            );

            var review = await instantVerificationService.VerifyOnceOrThrowAsync(instructions);
            if (review is FaultyReview faultyReview)
                TrainerContext.Suggestions.AttachRange(faultyReview.Suggestions);

            TrainerContext.Reviews.Attach(review);

            solution.SetReview(review);

            await TrainerContext.SaveChangesAsync();

            await new UpdateRankHelper(TrainerContext, rankService).UpdateRank(StudentId, assignment.ExerciseId);
        }
        catch
        {
            // ignored
        }

        return NoContent();
    }
}