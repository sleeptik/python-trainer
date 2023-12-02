using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.EducationAdmin.Notifications;
using WebApi.Features.EducationAdmin.Requests;

namespace WebApi.Features.EducationAdmin;

[Route("api/admin/education")]
public class EducationAdminController(IMediator mediator) : ApiController
{
    [HttpGet("unverified")]
    public async Task<IActionResult> GetUnverifiedExercises()
    {
        var exercises = await mediator.Send(new GetUnverifiedAssignmentsQuery());
        return Ok(exercises);
    }

    [HttpPatch("status")]
    public async Task<IActionResult> SetStatus(SetHistoryStatusCommand command)
    {
        await mediator.Send(command);

        var notification = new AssignmentVerifiedNotification(command.StudentId, command.ExerciseId);
        await mediator.Publish(notification);

        return NoContent();
    }
}