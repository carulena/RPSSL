using RpsslGameApi.Infrastructure.Mappers;

namespace RpsslGameApi.Infrastructure.Tests.Mappers;

[TestFixture]
public class PlayMapperTests
{
    private PlayMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _mapper = new PlayMapper();
    }

    [TestCase(1)]
    [TestCase(3)]
    [TestCase(5)]
    public void GetPlay_ReturnsPlayResponse_WhenInputIsValid(int player)
    {
        var randomNumber = 50;

        var result = _mapper.Play(randomNumber, player);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Player, Is.EqualTo(player));
        Assert.That(result.Computer, Is.InRange(1, 5));
        Assert.That(result.Results, Is.Not.Empty);
    }

    [TestCase("win")]
    [TestCase("lose")]
    [TestCase("tie")]
    public void GetPlay_ReturnsValidResult(string expected)
    {
        var validResults = new[] { "win", "lose", "tie" };
        var randomNumber = 50;

        var result = _mapper.Play(randomNumber, 1);

        Assert.That(validResults, Contains.Item(result.Results));
    }

    [TestCase(0)]
    [TestCase(6)]
    [TestCase(-1)]
    public void GetPlay_ThrowsArgumentOutOfRangeException_WhenPlayerIsInvalid(int player)
    {
        var randomNumber = 50;

        Assert.Throws<ArgumentOutOfRangeException>(() => _mapper.Play(randomNumber, player));
    }

    [TestCase(0)]
    [TestCase(101)]
    [TestCase(-1)]
    public void GetPlay_ThrowsArgumentOutOfRangeException_WhenRandomNumberIsInvalid(int number)
    {

        Assert.Throws<ArgumentOutOfRangeException>(() => _mapper.Play(number, 1));
    }
}