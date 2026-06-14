using RpsslGameApi.Contracts;
using Microsoft.Extensions.Caching.Memory;
using RpsslGameApi.Infrastructure.Services.Interfaces;

namespace RpsslGameApi.Infrastructure.Services;

public class ScoreboardService : IScoreboardService
{
    private readonly IMemoryCache _cache;
    private const string Key = "scoreboard";
    private const int MaxResults = 10;

    public ScoreboardService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public IEnumerable<PlayResponse> GetResults()
    {
        try
        {
            return _cache.TryGetValue(Key, out List<PlayResponse>? results)
                ? results!
                : [];
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to retrieve scoreboard: {e.Message}");
        }
    }

    public void AddResult(PlayResponse play)
    {
        if (play is null)
            throw new ArgumentNullException(nameof(play), "Play result cannot be null.");

        try
        {
            var results = GetResults().ToList();
            results.Insert(0, play);

            if (results.Count > MaxResults)
                results = results.Take(MaxResults).ToList();

            _cache.Set(Key, results);
        }
        catch (Exception e) when (e is not ArgumentNullException)
        {
            throw new Exception($"Failed to add result to scoreboard: {e.Message}");
        }
    }

    public bool Clear()
    {
        try
        {
            _cache.Remove(Key);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to clear scoreboard: {e.Message}");
        }
    }
}