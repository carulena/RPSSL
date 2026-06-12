using RpsslGameApi.Contracts;
using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Domain.Services;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Utils;

namespace RpsslGameApi.Infrastructure.Mappers;

public class PlayMapper : IPlayMapper
{
    public PlayResponse GetPlay(int randomNumber, int player)
    {
        if (player < 1 || player > 5)
            throw new ArgumentOutOfRangeException(nameof(player), "Player choice must be between 1 and 5.");

        if (randomNumber < 1 || randomNumber > 100)
            throw new ArgumentOutOfRangeException(nameof(randomNumber), "Random number must be between 1 and 100.");

        var computer = LinearInterpolation.Normalize(randomNumber);
        var result = GameRules.GetResult((Choice)player, (Choice)computer);

        return new PlayResponse
        {
            Computer = computer,
            Player = player,
            Result = result.ToString()
        };
    }
}