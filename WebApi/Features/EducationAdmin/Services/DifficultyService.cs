using Domain.Trainer;
using Infrastructure;

namespace WebApi.Features.EducationAdmin.Services;

public class DifficultyService : IAsyncDisposable, IDisposable
{
    private readonly Timer _difficultyUpdateTimer;
    private readonly IServiceScopeFactory _scopeFactory;

    private IReadOnlyList<Rank> _difficulties = Array.Empty<Rank>();

    public DifficultyService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _difficultyUpdateTimer = new Timer(
            _ => UpdateDifficulties(), null, TimeSpan.Zero, TimeSpan.FromMinutes(30)
        );
    }

    public async ValueTask DisposeAsync()
    {
        await _difficultyUpdateTimer.DisposeAsync();
    }

    public void Dispose()
    {
        _difficultyUpdateTimer.Dispose();
    }

    private void UpdateDifficulties()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _difficulties = context.Ranks.OrderBy(difficulty => difficulty.Id).ToList().AsReadOnly();
    }

    public int GetLowerDifficultyId(int currentDifficultyId)
    {
        if (_difficulties.Count == 0)
            throw new InvalidOperationException();

        var currentDifficulty = _difficulties.First(difficulty => difficulty.Id == currentDifficultyId);

        return _difficulties.Where(difficulty => difficulty.Id < currentDifficultyId)
            .LastOrDefault(currentDifficulty).Id;
    }

    public int GetHigherDifficultyId(int currentDifficultyId)
    {
        if (_difficulties.Count == 0)
            throw new InvalidOperationException();

        var currentDifficulty = _difficulties.First(difficulty => difficulty.Id == currentDifficultyId);

        return _difficulties.Where(difficulty => difficulty.Id > currentDifficultyId)
            .FirstOrDefault(currentDifficulty).Id;
    }
}