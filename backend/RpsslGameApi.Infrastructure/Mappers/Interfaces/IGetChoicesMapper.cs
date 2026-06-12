using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Mappers.Interfaces;

public interface IGetChoicesMapper
{
    public IEnumerable<ChoiceResponse> GetChoices();
}