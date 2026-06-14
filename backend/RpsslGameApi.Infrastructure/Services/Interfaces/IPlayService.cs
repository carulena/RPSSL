using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Services.Interfaces;

public interface IPlayService
{
    public Task<PlayResponse> PlayGame(int player);
}