using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.WebApi.Common;

namespace Trainer.WebApi.Controllers.Subjects;

[Route("api/subjects")]
public class SubjectsController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetSubjects()
    {
        var subjects = await TrainerContext.Subjects.AsNoTracking()
            .ToListAsync();

        return Ok(subjects);
    }
}