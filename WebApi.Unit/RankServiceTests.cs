using Domain.Trainer;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trainer.WebApi.Services;

namespace WebApi.Unit;

public class RankServiceTests
{
    private readonly ServiceProvider _provider;

    public RankServiceTests()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<ApplicationDbContext>(builder => builder.UseInMemoryDatabase("base"));
        serviceCollection.AddTransient<RankService>();

        _provider = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public void UpperBound_DbValid_ReturnsHighestRankId()
    {
        var context = _provider.GetRequiredService<ApplicationDbContext>();

        for (var i = 0; i < 10; i++)
        {
            context.Ranks.Add(Rank.Create($"Rank{i}", i * 10, (i + 1) * 10, 1));
            context.SaveChanges();

            var highestUpperBound = context.Ranks.Max(rank => rank.UpperBound);

            var rankService = _provider.GetRequiredService<RankService>();

            rankService.HighestUpperBound.Should().Be(highestUpperBound);
        }
    }

    [Fact]
    public void LowerBound_DbValid_ReturnsLowestRankId()
    {
        var context = _provider.GetRequiredService<ApplicationDbContext>();

        for (var i = 0; i < 10; i++)
        {
            context.Ranks.Add(Rank.Create($"Rank{i}", i * 10, (i + 1) * 10, 1));
            context.SaveChanges();

            var lowestUpperBound = context.Ranks.Min(rank => rank.LowerBound);

            var rankService = _provider.GetRequiredService<RankService>();

            rankService.LowestLowerBound.Should().Be(lowestUpperBound);
        }
    }
}