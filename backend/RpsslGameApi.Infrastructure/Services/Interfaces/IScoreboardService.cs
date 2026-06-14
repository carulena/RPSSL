using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Services.Interfaces;

public interface IScoreboardService
{
    public IEnumerable<PlayResponse> GetResults();
    public void AddResult(PlayResponse play);
    public bool Clear();
}   