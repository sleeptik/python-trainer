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
            .ThenInclude(exercise => exercise.Subjects)
            .Where(history => history.StudentId == userId)
            .OrderBy(history => history.FinishedAt)
            .ToList();
    }
}