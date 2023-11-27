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
        var exercises = await sender.Send(new GetNewExercisesRequest(subjectId));
        return Ok(exercises);
    }
}