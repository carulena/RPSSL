using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Services.Interfaces;

public interface IGetChoicesService
{
    public IEnumerable<ChoiceResponse> FetchChoices();
}