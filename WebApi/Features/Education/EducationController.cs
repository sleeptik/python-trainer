using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.Education.Requests;
using WebApi.Features.EducationAdmin.Notifications;

namespace WebApi.Features.Education;

[Route("api/education")]
public sealed class EducationController(IMediator mediator) : ApiController
{
    [HttpGet("new")]
    public async Task<IActionResult> GetNewExercise(int subjectId)
    {
        var exercises = await mediator.Send(new GetNewExerciseRequest(subjectId));
        return Ok(exercises);
    }

    [HttpPatch("finish")]
    public async Task<IActionResult> FinishExercise(FinishExerciseCommand command)
    {
        var result = await mediator.Send(command);
        
        var notification = new AssignmentVerifiedNotification(command.StudentId, command.ExerciseId);
        await mediator.Publish(notification);
        return Ok(result);
    }
}