using Domain.Trainer;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.Education.Services;

public class HistoryService(ApplicationDbContext context)
{
    public IList<History> GetUserHistory(int userId)
    {
        return context.Histories.AsNoTracking()
            .Include(history => history.Exercise)
            .Where(history => history.UserId == userId)
            .OrderBy(history => history.Finished)
            .ToList();
    }
}