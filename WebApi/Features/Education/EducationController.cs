using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.Education.GetAssignments;
using WebApi.Features.Education.SelfAssignment;
using WebApi.Features.Education.SetAssignmentSolution;
using WebApi.Features.EducationAdmin.Notifications;

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

    [HttpPost("")]
    public async Task<IActionResult> AddSelfAssignment(SelfAssignmentRequest request)
    {
        var newAssignment = await mediator.Send(request with { StudentId = 1 });
        return Ok(newAssignment);
    }

    [HttpPatch("")]
    public async Task<IActionResult> SetAssignmentSolution(SetAssignmentSolutionRequest request)
    {
        var result = await mediator.Send(request);

        var notification = new AssignmentVerifiedNotification(request.StudentId, request.ExerciseId);
        await mediator.Publish(notification);

        return Ok(result);
    }
}