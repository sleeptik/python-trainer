using Microsoft.AspNetCore.Mvc;
using Trainer.Database;

namespace Trainer.WebApi.Common;

[ApiController]
public class ApiController : ControllerBase
{
    private TrainerContext? _trainerContext;

    protected TrainerContext TrainerContext
    {
        get
        {
            _trainerContext ??= HttpContext.RequestServices.GetRequiredService<TrainerContext>();
            return _trainerContext;
        }
    }
}