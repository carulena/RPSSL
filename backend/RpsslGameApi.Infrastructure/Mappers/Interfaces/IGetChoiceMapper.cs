using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Mappers.Interfaces;

public interface IGetChoiceMapper
{
    public ChoiceResponse GetChoice(int randomNumber);
}