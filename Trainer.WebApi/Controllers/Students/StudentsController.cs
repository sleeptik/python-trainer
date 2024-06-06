using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.WebApi.Common;

namespace Trainer.WebApi.Controllers.Students;

[Route("api/education/students")]
public sealed class StudentsController : ApiController
{
    private int StudentId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1");
    
    [HttpGet("me")]
    public async Task<IActionResult> GetMeStudent()
    {
        var student = await TrainerContext.Students.AsNoTracking()
            .FirstAsync(s => s.UserId == StudentId);
        
        return Ok(student);
    }
}