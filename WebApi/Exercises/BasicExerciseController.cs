using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Exercises;
[Route("education")]
public sealed class BasicExerciseController(ISender sender) : ApiController
{
    [HttpGet("new")]
    public async Task<IActionResult> GetExerciseBySubjectAndDifficulty(int subjectId,int difficultyId)
    {
        var exercises = await sender.Send(new GetExerciseRequest(subjectId,difficultyId));
        return Ok(exercises);
    }

}