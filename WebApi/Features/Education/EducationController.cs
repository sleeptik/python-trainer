using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Features.Education.Requests;

namespace WebApi.Features.Education;

[Route("api/education")]
public sealed class EducationController(ISender sender) : ApiController
{
    [HttpGet("new")]
    public async Task<IActionResult> GetNewExercise(int subjectId)
    {
        var exercises = await sender.Send(new GetNewExerciseRequest(subjectId));
        return Ok(exercises);
    }

    [HttpPatch("finish")]
    public async Task<IActionResult> FinishExercise(FinishExerciseCommand command)
    {
        await sender.Send(command);
        return NoContent();
    }
}