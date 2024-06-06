using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.WebApi.Common;

namespace Trainer.WebApi.Controllers.Exercises;

[Route("api/education/exercises")]
public sealed class ExercisesController: ApiController
{
    [HttpGet("{exerciseId:int}/code-template")]
    public async Task<IActionResult> GetCodeTemplate(int exerciseId)
    {
        var codeTemplates = await TrainerContext.CodeTemplates.AsNoTracking()
            .Where(template => template.ExerciseId == exerciseId)
            .ToListAsync();
        var codeTemplate = codeTemplates.OrderBy(_ => Random.Shared.Next())
            .First();
        return Ok(codeTemplate);
    }
}