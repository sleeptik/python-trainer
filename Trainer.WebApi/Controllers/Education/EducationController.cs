using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.WebApi.Common;
using Trainer.WebApi.Controllers.Education.DTO;
using Trainer.WebApi.Features.Education.GetAssignmentDetails;
using Trainer.WebApi.Features.Education.GetStudentAssignmentList;
using Trainer.WebApi.Features.Education.GetStudentSubjectList;
using Trainer.WebApi.Features.Education.SetAssignmentSolution;
using Trainer.WebApi.Features.Education.StudentSelfAssignment;

namespace Trainer.WebApi.Features.Education;

[Route("api/education")]
public sealed class EducationController(IMediator mediator) : ApiController
{
    private int StudentId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1");

    [HttpGet("")]
    public async Task<IActionResult> GetStudentAssignmentList()
    {
        var exercises = await mediator.Send(new GetStudentAssignmentListRequest(StudentId));
        return Ok(exercises);
    }

    [HttpGet("exercises/{exerciseId:int}")]
    public async Task<IActionResult> GetAssignmentDetails(GetAssignmentDetailsRequest detailsRequest)
    {
        var assignment = await TrainerContext.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Rank)
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .SingleAsync(
                assignment => assignment.StudentId == detailsRequest.StudentId
                              && assignment.ExerciseId == detailsRequest.ExerciseId
            );

        return Ok(assignment);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddSelfAssignment(StudentSelfAssignmentDto dto)
    {
        var newAssignment = await mediator.Send(new StudentSelfAssignmentRequest(StudentId, dto.SubjectId));
        return Ok(newAssignment);
    }

    [HttpPatch("")]
    public async Task<IActionResult> SetAssignmentSolution(SetAssignmentSolutionDto dto)
    {
        var result = await mediator.Send(new SetAssignmentSolutionRequest(StudentId, dto.ExerciseId, dto.Solution));

        var notification = new AssignmentSolutionVerifiedNotification(StudentId, dto.ExerciseId);
        await mediator.Publish(notification);

        return Ok(result);
    }

    [HttpGet("subjects")]
    public async Task<IActionResult> GetStudentSubjectList()
    {
        var subjects = await mediator.Send(new GetStudentSubjectListRequest(StudentId));
        return Ok(subjects);
    }
}