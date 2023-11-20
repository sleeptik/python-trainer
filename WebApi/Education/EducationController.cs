using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Education;

[Route("education")]
public sealed class EducationController(ISender sender) : ApiController
{
    [HttpGet("new")]
    public async Task<IActionResult> GetExerciseBySubjectAndDifficulty(int subjectId, int difficultyId)
    {
        var exercises = await sender.Send(new GetExerciseRequest(subjectId));
        return Ok(exercises);
    }
}