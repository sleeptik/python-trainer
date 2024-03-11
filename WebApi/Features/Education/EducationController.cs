using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.Education.GetAssignmentDetails;
using WebApi.Features.Education.GetStudentAssignmentList;
using WebApi.Features.Education.GetStudentSubjectList;
using WebApi.Features.Education.SetAssignmentSolution;
using WebApi.Features.Education.StudentSelfAssignment;

namespace WebApi.Features.Education;

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

    [HttpGet("{exerciseId:int}")]
    public async Task<IActionResult> GetAssignmentDetails(GetAssignmentDetailsDto dto)
    {
        var assignment = await mediator.Send(new GetAssignmentDetailsRequest(StudentId, dto.ExerciseId));
        return Ok(assignment);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddSelfAssignment(StudentSelfAssignmentDto dto)
    {
        var newAssignment = await mediator.Send(new StudentSelfAssignmentRequest(StudentId, dto.SubjectId));
        return newAssignment is not null ? Ok(newAssignment) : StatusCode(501);
    }

    [HttpPatch("")]
    public async Task<IActionResult> SetAssignmentSolution(SetAssignmentSolutionRequest request)
    {
        var result = await mediator.Send(request);

        var notification = new AssignmentSolutionVerifiedNotification(request.StudentId, request.ExerciseId);
        await mediator.Publish(notification);

        return result is not null ? Ok(result) : StatusCode(501);
    }

    [HttpGet("subjects")]
    public async Task<IActionResult> GetStudentSubjectList()
    {
        var subjects = await mediator.Send(new GetStudentSubjectListRequest(1));
        return subjects is not null ? Ok(subjects) : StatusCode(501);
    }
}