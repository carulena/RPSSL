using RpsslGameApi.Contracts;
using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
namespace RpsslGameApi.Infrastructure.Mappers;

public class GetChoicesMapper:IGetChoicesMapper
{
    public GetChoicesMapper(){}

    public IEnumerable<ChoiceResponse> GetChoices()
    {
        return Enum.GetValues<Choice>()
            .Select(c => new ChoiceResponse
            {
                Id = (int)c,
                Name = c.ToString()
            });
    }
}