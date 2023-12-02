using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.EducationAdmin.Notifications;
using WebApi.Features.EducationAdmin.Requests;

namespace WebApi.Features.EducationAdmin;

[Route("api/admin/education")]
public class EducationAdminController(IMediator mediator) : ApiController
{
    [HttpGet("status")]
    public async Task<IActionResult> GetUnverifiedExercises([FromQuery] GetAssignmentStatusQuery query)
    {
        var exercises = await mediator.Send(query);
        return Ok(exercises);
    }

    [HttpPut("status")]
    public async Task<IActionResult> SetStatus(SetHistoryStatusCommand command)
    {
        await mediator.Send(command);

        var notification = new AssignmentVerifiedNotification(command.UserId, command.ExerciseId, command.Status);
        await mediator.Publish(notification);

        return NoContent();
    }
}