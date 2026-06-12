using Moq;
using RpsslGameApi.Contracts;
using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Services;

namespace RpsslGameApi.Infrastructure.Tests.Services;

[TestFixture]
public class GetChoicesServiceTests
{
    private Mock<IGetChoicesMapper> _choicesMapperMock;
    private GetChoicesService _service;

    [SetUp]
    public void SetUp()
    {
        _choicesMapperMock = new Mock<IGetChoicesMapper>();
        _service = new GetChoicesService(_choicesMapperMock.Object);
    }

    [Test]
    public void FetchChoices_ReturnsAllChoices_WhenMapperReturnsAll()
    {
        var choices = Enum.GetValues<Choice>().Select(ChoiceResponse.FromChoice).ToList();
        _choicesMapperMock.Setup(m => m.GetChoices()).Returns(choices);

        var result = _service.FetchChoices().ToList();

        Assert.That(result, Has.Count.EqualTo(5));
        Assert.That(result, Is.EquivalentTo(choices));
    }

    [Test]
    public void FetchChoices_ReturnsEmpty_WhenMapperReturnsEmpty()
    {
        _choicesMapperMock.Setup(m => m.GetChoices()).Returns([]);

        var result = _service.FetchChoices().ToList();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void FetchChoices_ThrowsException_WhenMapperThrows()
    {
        _choicesMapperMock.Setup(m => m.GetChoices()).Throws(new Exception("Mapper error"));

        Assert.Throws<Exception>(() => _service.FetchChoices());
    }
}