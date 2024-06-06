using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.WebApi.Common;

namespace Trainer.WebApi.Controllers.Exercises;

[Route("api/education/exercises")]
public sealed class ExercisesController: ApiController
{
    [HttpGet("{exerciseId:int}/code-template")]
    public async Task<IActionResult> GetCodeTemplates(int exerciseId)
    {
        var codeTemplates = await TrainerContext.CodeTemplates.AsNoTracking()
            .Where(template => template.ExerciseId == exerciseId)
            .ToListAsync();
        return Ok(codeTemplates);
    }
}