using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Services.Interfaces;

public interface IGetChoiceService
{
    public Task<ChoiceResponse> FetchChoice();
}