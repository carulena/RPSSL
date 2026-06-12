using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Mappers.Interfaces;

public interface IPlayMapper
{
    public PlayResponse GetPlay(int randomNumber, int player);
}