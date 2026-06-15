using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Domain.Services;

namespace RpsslGameApi.Domain.Tests;

[TestFixture]
public class GameRulesTest
{
    [Test]
    public void GetResult_ReturnsTie_WhenBothChoicesAreEqual()
    {
        foreach (var choice in Enum.GetValues<Choice>())
        {
            var result = GameRules.GetResult(choice, choice);
            Assert.That(result, Is.EqualTo(GameResult.Tie));
        }
    }

    [TestCase(Choice.Rock,     Choice.Scissors, GameResult.Win)]
    [TestCase(Choice.Rock,     Choice.Lizard,   GameResult.Win)]
    [TestCase(Choice.Paper,    Choice.Rock,     GameResult.Win)]
    [TestCase(Choice.Paper,    Choice.Spock,    GameResult.Win)]
    [TestCase(Choice.Scissors, Choice.Paper,    GameResult.Win)]
    [TestCase(Choice.Scissors, Choice.Lizard,   GameResult.Win)]
    [TestCase(Choice.Spock,    Choice.Rock,     GameResult.Win)]
    [TestCase(Choice.Spock,    Choice.Scissors, GameResult.Win)]
    [TestCase(Choice.Lizard,   Choice.Spock,    GameResult.Win)]
    [TestCase(Choice.Lizard,   Choice.Paper,    GameResult.Win)]
    public void GetResult_ReturnsWin_WhenPlayerBeatsComputer(Choice player, Choice computer, GameResult expected)
    {
        var result = GameRules.GetResult(player, computer);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(Choice.Scissors, Choice.Rock,     GameResult.Lose)]
    [TestCase(Choice.Lizard,   Choice.Rock,     GameResult.Lose)]
    [TestCase(Choice.Rock,     Choice.Paper,    GameResult.Lose)]
    [TestCase(Choice.Spock,    Choice.Paper,    GameResult.Lose)]
    [TestCase(Choice.Paper,    Choice.Scissors, GameResult.Lose)]
    [TestCase(Choice.Lizard,   Choice.Scissors, GameResult.Lose)]
    [TestCase(Choice.Rock,     Choice.Spock,    GameResult.Lose)]
    [TestCase(Choice.Scissors, Choice.Spock,    GameResult.Lose)]
    [TestCase(Choice.Spock,    Choice.Lizard,   GameResult.Lose)]
    [TestCase(Choice.Paper,    Choice.Lizard,   GameResult.Lose)]
    public void GetResult_ReturnsLose_WhenComputerBeatsPlayer(Choice player, Choice computer, GameResult expected)
    {
        var result = GameRules.GetResult(player, computer);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void GetResult_ThrowsKeyNotFoundException_WhenChoiceIsInvalid()
    {
        var invalid = (Choice)99;
        Assert.Throws<KeyNotFoundException>(() => GameRules.GetResult(invalid, Choice.Rock));
    }
}