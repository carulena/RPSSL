using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Utils;

namespace RpsslGameApi.Infrastructure.Mappers;

public class GetChoiceMapper : IGetChoiceMapper
{
    public ChoiceResponse GetChoice(int randomNumber)
    {
        if (randomNumber < 1 || randomNumber > 100)
            throw new ArgumentOutOfRangeException(nameof(randomNumber), "Random number must be between 1 and 100.");

        var number = LinearInterpolation.Normalize(randomNumber);
        return ChoiceResponse.FromId(number);
    }
}