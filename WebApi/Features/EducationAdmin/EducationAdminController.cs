using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.EducationAdmin.Requests;

namespace WebApi.Features.EducationAdmin;

[Route("api/admin/education")]
public class EducationAdminController(ISender sender) : ApiController
{
    [HttpGet("status")]
    public async Task<IActionResult> GetUnverifiedExercises(GetExerciseHistoryStatus status)
    {
        var exercises = await sender.Send(status);
        return Ok(exercises);
    }

    [HttpPut("status")]
    public async Task<IActionResult> SetStatus(SetHistoryStatusCommand statusCommand)
    {
        await sender.Send(statusCommand);
        return NoContent();
    }
}