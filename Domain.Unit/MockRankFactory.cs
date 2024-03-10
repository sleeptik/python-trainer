using Domain.Trainer;

namespace Domain.Unit;

public class MockRankFactory
{
    public static IEnumerable<Rank> CreateEmpty()
    {
        return Enumerable.Empty<Rank>();
    }
    
    public static IEnumerable<Rank> CreateSingle()
    {
        return new[] { Rank.Create("Base", 0, 10, 1) };
    }

    public static IEnumerable<Rank> CreateMultiple()
    {
        return new[]
        {
            Rank.Create("Low", 0, 10, 1),
            Rank.Create("Mid", 10, 20, 1),
            Rank.Create("High", 20, 30, 1)
        };
    }
}