using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.Education.GetAssignment;
using WebApi.Features.Education.GetAssignments;
using WebApi.Features.Education.GetSubjects;
using WebApi.Features.Education.SelfAssignment;
using WebApi.Features.Education.SetAssignmentSolution;

namespace WebApi.Features.Education;

[Route("api/education")]
public sealed class EducationController(IMediator mediator) : ApiController
{
    [HttpGet("")]
    public async Task<IActionResult> GetMyAssignments()
    {
        var exercises = await mediator.Send(new GetAssignmentsRequest(1));
        return Ok(exercises);
    }

    [HttpGet("{exerciseId:int}")]
    public async Task<IActionResult> GetAssignment(int exerciseId)
    {
        var assignment = await mediator.Send(new GetAssignmentRequest(1, exerciseId));
        return Ok(assignment);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddSelfAssignment(SelfAssignmentRequest request)
    {
        var newAssignment = await mediator.Send(request with { StudentId = 1 });
        
        return newAssignment is not null ? Ok(newAssignment): StatusCode(501);
    }

    [HttpPatch("")]
    public async Task<IActionResult> SetAssignmentSolution(SetAssignmentSolutionRequest request)
    {
        var result = await mediator.Send(request);

        var notification = new AssignmentSolutionVerifiedNotification(request.StudentId, request.ExerciseId);
        await mediator.Publish(notification);

        return result is not null ? Ok(result): StatusCode(501);
    }

    [HttpGet("subjects")]
    public async Task<IActionResult> GetMySubjects()
    {
        var subjects = await mediator.Send(new GetSubjectsRequest(1));
        return subjects is not null ? Ok(subjects): StatusCode(501);
    }
}