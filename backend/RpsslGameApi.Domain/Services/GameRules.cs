using RpsslGameApi.Domain.Entities;
namespace RpsslGameApi.Domain.Services;

public static class GameRules
{
    private static readonly Dictionary<Choice, List<Choice>> Wins = new()
    {
        { Choice.Rock,     [Choice.Scissors, Choice.Lizard] },
        { Choice.Paper,    [Choice.Rock,     Choice.Spock] },
        { Choice.Scissors, [Choice.Paper,    Choice.Lizard] },
        { Choice.Spock,    [Choice.Rock,     Choice.Scissors] },
        { Choice.Lizard,   [Choice.Spock,    Choice.Paper] },
    };

    public static GameResult GetResult(Choice player, Choice computer)
    {
        if (!Wins.ContainsKey(player))
            throw new KeyNotFoundException($"Invalid choice: {player}");
        if (player == computer) return GameResult.Tie;
        return Wins[player].Contains(computer) ? GameResult.Win : GameResult.Lose;
    }
}
