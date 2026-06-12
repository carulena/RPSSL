using Moq;
using RpsslGameApi.Contracts;
using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Repositories.Interface;
using RpsslGameApi.Infrastructure.Services;

namespace RpsslGameApi.Infrastructure.Tests.Services;

[TestFixture]
public class GetChoiceServiceTest
{
    private Mock<IGetChoiceMapper> _choiceMapperMock;
    private Mock<IGetRandomNumberRepository> _randomNumberRepositoryMock;
    private GetChoiceService _service;

    [SetUp]
    public void SetUp()
    {
        _choiceMapperMock = new Mock<IGetChoiceMapper>();
        _randomNumberRepositoryMock = new Mock<IGetRandomNumberRepository>();
        _service = new GetChoiceService(_choiceMapperMock.Object, _randomNumberRepositoryMock.Object);
    }

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    public async Task FetchChoice_ReturnsChoiceResponse_WhenRandomNumberIsValid(int randomNumber)
    {
        var expected = ChoiceResponse.FromChoice(Choice.Rock);
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(randomNumber);
        _choiceMapperMock.Setup(m => m.GetChoice(randomNumber)).Returns(expected);

        var result = await _service.FetchChoice();

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0)]
    [TestCase(101)]
    [TestCase(-1)]
    public void FetchChoice_ThrowsArgumentOutOfRangeException_WhenRandomNumberIsInvalid(int randomNumber)
    {
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(randomNumber);

        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.FetchChoice());
    }

    [Test]
    public void FetchChoice_ThrowsException_WhenRepositoryThrows()
    {
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ThrowsAsync(new Exception("Repository error"));

        Assert.ThrowsAsync<Exception>(() => _service.FetchChoice());
    }

    [Test]
    public void FetchChoice_ThrowsException_WhenMapperThrows()
    {
        _randomNumberRepositoryMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(50);
        _choiceMapperMock.Setup(m => m.GetChoice(50)).Throws(new ArgumentOutOfRangeException());

        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.FetchChoice());
    }
}