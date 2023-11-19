using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Exercises;
[Route("api")]
public sealed class BasicExerciseController(ISender sender) : ApiController
{
    [HttpGet("get")]
    public async Task<IActionResult> GetExerciseBySubjectAndDifficulty([FromQuery] int subjectId,[FromQuery] int difficultyId)
    {
        var exercises = await sender.Send(new GetExerciseRequest(subjectId,difficultyId));
        return Ok(exercises);
    }

}