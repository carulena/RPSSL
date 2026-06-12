using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Services.Interfaces;
namespace RpsslGameApi.Infrastructure.Services;

public class GetChoicesService:IGetChoicesService
{
    private readonly IGetChoicesMapper _choicesMapper;

    public GetChoicesService(IGetChoicesMapper choicesMapper)
    {
        _choicesMapper = choicesMapper;
    }
    
    public IEnumerable<ChoiceResponse> FetchChoices()
    {
        return _choicesMapper.GetChoices();
    }
}