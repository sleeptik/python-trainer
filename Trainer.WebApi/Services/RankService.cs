using Microsoft.EntityFrameworkCore;
using Trainer.Database;

namespace Trainer.WebApi.Services;

public sealed class RankService(TrainerContext context)
{
    //Сервис для изменения ранга пользователя
    
    //Переменные для получения верхней границы верхнего ранга и нижней границы нижнего ранга
    //Они необходимы для изменения рейтинга во время подсчёта коэффициента завершения тренажера пользователем
    public float HighestUpperBound => context.Ranks.Select(rank => rank.UpperBound).Max();
    public float LowestLowerBound => context.Ranks.Select(rank => rank.LowerBound).Min();

    //Метод изменяющий RankId в зависимости от текущего рейтинга пользователя
    public async Task<int> GetUpdatedRankId(int currentRankId, float score, CancellationToken cancellationToken)
    {
        var ranks = await context.Ranks.AsNoTracking()
            .Where(rank =>
                (rank.LowerBound <= score && score <= rank.UpperBound)
                || (score < rank.LowerBound && score >= rank.LowerBound - rank.FallThreshold)
            )
            .ToListAsync(cancellationToken);

        return ranks.Count switch
        {
            2 => currentRankId,
            1 => ranks.First().Id,
            _ => throw new InvalidOperationException()
        };
    }
}