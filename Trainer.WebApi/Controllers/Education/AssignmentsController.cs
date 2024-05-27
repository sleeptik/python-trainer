using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification.InputData;
using Trainer.WebApi.Common;
using Trainer.WebApi.Controllers.Education.DTO;
using Trainer.WebApi.Controllers.Education.StudentSelfAssignment;
using Trainer.WebApi.Services;

namespace Trainer.WebApi.Controllers.Education;

[Route("api/education/assignments")]
public sealed class AssignmentsController(InstantVerificationService instantVerificationService) : ApiController
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
            .SingleAsync(assignment => assignment.Id == assignmentId);

        return Ok(assignment);
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
            .SingleAsync(assignment1 => assignment1.Id == assignmentId);

        var solution = Solution.Create(dto.Solution);
        assignment.AddSolution(solution);

        await TrainerContext.SaveChangesAsync();

        try
        {
            var customInstructions = TrainerContext.Prompts
                .Where(prompt => solution.Assignment.Exercise.Subjects.Any(subject => subject.Id == prompt.SubjectId))
                .Select(prompt => prompt.Content)
                .ToList();

            var instructions = new VerificationInstructionsSet(
                assignment.Exercise.Details, dto.Solution, customInstructions
            );

            var review = await instantVerificationService.VerifyOnceOrThrowAsync(instructions);
            solution.SetReview(review);

            await TrainerContext.SaveChangesAsync();
        }
        catch
        {
            // ignored
        }

        return NoContent();
    }
}