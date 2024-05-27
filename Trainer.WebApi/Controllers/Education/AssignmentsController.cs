using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Assignments;
using Trainer.WebApi.Common;
using Trainer.WebApi.Controllers.Education.DTO;
using Trainer.WebApi.Controllers.Education.StudentSelfAssignment;

namespace Trainer.WebApi.Controllers.Education;

[Route("api/education/assignments")]
public sealed class AssignmentsController : ApiController
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
        var assignment = await TrainerContext.Assignments.SingleAsync(assignment1 => assignment1.Id == assignmentId);

        var solution = Solution.Create(dto.Solution);
        assignment.AddSolution(solution);

        await TrainerContext.SaveChangesAsync();

        return NoContent();
    }
}