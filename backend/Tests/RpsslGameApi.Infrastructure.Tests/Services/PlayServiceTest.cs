using Moq;
using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Repositories.Interface;
using RpsslGameApi.Infrastructure.Services;
using RpsslGameApi.Infrastructure.Services.Interfaces;

namespace RpsslGameApi.Infrastructure.Tests.Services;

[TestFixture]
public class PlayServiceTests
{
    private Mock<IPlayMapper> _playMapperMock;
    private Mock<IGetRandomNumberRepository> _randomNumberRepositoryMock;
    private Mock<IScoreboardService> _scoreboardServiceMock;
    private PlayService _service;

    [SetUp]
    public void SetUp()
    {
        _playMapperMock = new Mock<IPlayMapper>();
        _randomNumberRepositoryMock = new Mock<IGetRandomNumberRepository>();
        _scoreboardServiceMock = new Mock<IScoreboardService>();
        _service = new PlayService(_playMapperMock.Object, _randomNumberRepositoryMock.Object, _scoreboardServiceMock.Object);
    }

    [TestCase(1)]
    [TestCase(3)]
    [TestCase(5)]
    public async Task FetchPlay_ReturnsPlayResponse_WhenInputIsValid(int player)
    {
        var expected = new PlayResponse { Results = "Win", Player = player, Computer = 1 };
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(50);
        _playMapperMock.Setup(m => m.Play(50, player)).Returns(expected);

        var result = await _service.PlayGame(player);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0)]
    [TestCase(6)]
    [TestCase(-1)]
    public void FetchPlay_ThrowsArgumentOutOfRangeException_WhenPlayerIsInvalid(int player)
    {
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.PlayGame(player));
    }

    [Test]
    public void FetchPlay_ThrowsException_WhenRepositoryThrows()
    {
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ThrowsAsync(new Exception("Repository error"));

        Assert.ThrowsAsync<Exception>(() => _service.PlayGame(1));
    }

    [Test]
    public void FetchPlay_ThrowsException_WhenMapperThrows()
    {
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(50);
        _playMapperMock.Setup(m => m.Play(50, 1)).Throws(new ArgumentOutOfRangeException());

        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.PlayGame(1));
    }

    [Test]
    public void FetchPlay_ThrowsTimeoutException_WhenRepositoryTimesOut()
    {
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ThrowsAsync(new TimeoutException("Request timed out."));

        Assert.ThrowsAsync<TimeoutException>(() => _service.PlayGame(1));
    }
}