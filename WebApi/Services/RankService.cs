using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services;

public sealed class RankService(ApplicationDbContext context)
{
    public float HighestUpperBound => context.Ranks.Select(rank => rank.UpperBound).Max();
    public float LowestLowerBound => context.Ranks.Select(rank => rank.LowerBound).Min();

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