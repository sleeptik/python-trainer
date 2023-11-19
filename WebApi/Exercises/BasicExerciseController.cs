using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Exercises;
[Route("api")]
public sealed class BasicExerciseController(ISender sender) : ApiController
{
    [HttpGet("get")]
    public async Task<IActionResult> GetExerciseBySubjectAndDifficulty(GetExerciseRequest request)
    {
        var exercises = await sender.Send(request);
        return Ok(exercises);
    }

}