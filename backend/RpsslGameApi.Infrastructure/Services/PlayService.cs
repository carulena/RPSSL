using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Repositories.Interface;
using RpsslGameApi.Infrastructure.Services.Interfaces;
namespace RpsslGameApi.Infrastructure.Services;

public class PlayService:IPlayService
{
    private readonly IPlayMapper _playMapper;
    private readonly IGetRandomNumberRepository _randomNumberRepository;
    public PlayService(
        IPlayMapper playMapper,
        IGetRandomNumberRepository randomNumberRepository
    )
    {
        _playMapper = playMapper;
        _randomNumberRepository = randomNumberRepository;
    }
    
    public async Task<PlayResponse> FetchPlay(int player)
    {
        if (player < 1 || player > 5)
            throw new ArgumentOutOfRangeException(nameof(player), "Player choice must be between 1 and 5.");

        var randomNumber = await _randomNumberRepository.GetRandomNumberAsync();
        return _playMapper.GetPlay(randomNumber, player);
    }
}