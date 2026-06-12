using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Repositories.Interface;
using RpsslGameApi.Infrastructure.Services.Interfaces;
namespace RpsslGameApi.Infrastructure.Services;

public class GetChoiceService:IGetChoiceService
{
    private readonly IGetChoiceMapper _choiceMapper;
    private readonly IGetRandomNumberRepository _randomNumberRepository;
    public GetChoiceService(
        IGetChoiceMapper choiceMapper,
        IGetRandomNumberRepository randomNumberRepository
        )
    {
        _choiceMapper = choiceMapper;
        _randomNumberRepository = randomNumberRepository;
    }
    
    public async Task<ChoiceResponse> FetchChoice()
    {
        var randomNumber = await _randomNumberRepository.GetRandomNumberAsync();
    
        if (randomNumber < 1 || randomNumber > 100)
            throw new ArgumentOutOfRangeException(nameof(randomNumber), "Random number must be between 1 and 100.");

        return _choiceMapper.GetChoice(randomNumber);
    }
}