using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Repositories.Interface;
using RpsslGameApi.Infrastructure.Services.Interfaces;
namespace RpsslGameApi.Infrastructure.Services;

public class PlayService:IPlayService
{
    private readonly IPlayMapper _playMapper;
    private readonly IGetRandomNumberRepository _randomNumberRepository;
    private readonly IScoreboardService _scoreboardService;
    public PlayService(
        IPlayMapper playMapper,
        IGetRandomNumberRepository randomNumberRepository,
        IScoreboardService scoreboardService
    )
    {
        _playMapper = playMapper;
        _randomNumberRepository = randomNumberRepository;
        _scoreboardService = scoreboardService;
    }
    
    public async Task<PlayResponse> PlayGame(int player)
    {
        if (player < 1 || player > 5)
            throw new ArgumentOutOfRangeException(nameof(player), "Player choice must be between 1 and 5.");

        var randomNumber = await _randomNumberRepository.GetRandomNumberAsync();
        var result = _playMapper.Play(randomNumber, player);
    
        _scoreboardService.AddResult(result);

        return result;
    }
}